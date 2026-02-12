using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.Rental;
using BikeRental.Application.Contracts.Renter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Net;

namespace BikeRental.Infrastructure.Nats;

/// <summary>
/// Фоновый сервис, читающий сообщения из NATS и создающий записи об аренде.
/// </summary>
public class RentalConsumer(
    IOptions<NatsConsumerSettings> options,
    INatsConnection natsConnection,
    IServiceProvider serviceProvider,
    ILogger<RentalConsumer> logger) : BackgroundService
{
    private readonly NatsConsumerSettings _settings = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("NATS Consumer запускается для потока {Stream}, тема {Subject}...",
            _settings.StreamName, _settings.SubjectName);

        var js = natsConnection.CreateJetStreamContext();

        var consumer = await js.CreateOrUpdateConsumerAsync(_settings.StreamName, new ConsumerConfig
        {
            Name = _settings.ConsumerName,
            DurableName = _settings.ConsumerName,
            FilterSubject = _settings.SubjectName,
            AckPolicy = ConsumerConfigAckPolicy.Explicit,
        }, cancellationToken: stoppingToken);

        logger.LogInformation("NATS Consumer {Name} готов к работе.", _settings.ConsumerName);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var fetchOpts = new NatsJSFetchOpts
                {
                    MaxMsgs = _settings.BatchSize,
                    Expires = TimeSpan.FromSeconds(_settings.BatchTimeoutSeconds)
                };

                var messages = new List<NatsJSMsg<RentalCreateUpdateDto>>();

                await foreach (var msg in consumer.FetchAsync<RentalCreateUpdateDto>(fetchOpts, cancellationToken: stoppingToken))
                {
                    if (msg.Data != null)
                    {
                        messages.Add(msg);
                    }
                    else
                    {
                        logger.LogWarning("Получено пустое сообщение, пропускаем.");
                        await msg.AckAsync(cancellationToken: stoppingToken);
                    }
                }

                if (messages.Count > 0)
                {
                    await ProcessBatchAsync(messages, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                logger.LogWarning("Получение записей об аренде отменено, сервис останавливается.");
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при потреблении сообщений из NATS. Ждем 5 секунд перед повтором.");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    private async Task ProcessBatchAsync(List<NatsJSMsg<RentalCreateUpdateDto>> messages, CancellationToken token)
    {
        logger.LogInformation("Обработка батча из {Count} сообщений...", messages.Count);

        using var scope = serviceProvider.CreateScope();
        var rentalService = scope.ServiceProvider.GetRequiredService<IApplicationService<RentalDto, RentalCreateUpdateDto, int>>();
        var bikeService = scope.ServiceProvider.GetRequiredService<IApplicationService<BikeDto, BikeCreateUpdateDto, int>>();
        var renterService = scope.ServiceProvider.GetRequiredService<IApplicationService<RenterDto, RenterCreateUpdateDto, int>>();

        foreach (var msg in messages)
        {
            try
            {
                await bikeService.Get(msg.Data!.BikeId);
                await renterService.Get(msg.Data!.RenterId);

                await rentalService.Create(msg.Data!);

                await msg.AckAsync(cancellationToken: token);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError(ex, "Ошибка при обработке сообщения (Rental), связанный арендатор или велосипед не найдены. Данные: {@Dto}", msg.Data);

                await msg.AckTerminateAsync(cancellationToken: token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обработке сообщения (Rental). Данные: {@Dto}", msg.Data);

                await msg.NakAsync(delay: TimeSpan.FromSeconds(20), cancellationToken: token);
            }
        }

        logger.LogInformation("Батч обработан.");
    }
}