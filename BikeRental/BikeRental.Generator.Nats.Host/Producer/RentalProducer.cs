using BikeRental.Application.Contracts.Rental;
using BikeRental.Generator.Nats.Host.Configuration;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Client.Serializers.Json;
using NATS.Net;

namespace BikeRental.Generator.Nats.Host.Producer;

/// <summary>
/// Отвечает за публикацию сгенерированных данных аренды в NATS JetStream.
/// </summary>
/// <param name="logger">Логгер.</param>
/// <param name="natsConnection">Подключение к NATS.</param>
/// <param name="settings">Настройки продюсера (имя стрима, тема).</param>
public class RentalProducer(
    ILogger<RentalProducer> logger,
    INatsConnection natsConnection,
    IOptions<NatsProducerSettings> settings)
{
    private INatsJSContext? _natsJsContext;
    private readonly NatsProducerSettings _settings = settings.Value;

    /// <summary>
    /// Проверяет наличие JetStream потока и создает его при отсутствии.
    /// </summary>
    public async Task EnsureStreamCreatedAsync(CancellationToken token = default)
    {
        try
        {
            _natsJsContext = natsConnection.CreateJetStreamContext();

            var streamConfig = new StreamConfig
            {
                Name = _settings.StreamName,
                Subjects = [_settings.SubjectName],
                Storage = StreamConfigStorage.Memory,
                Retention = StreamConfigRetention.Workqueue
            };

            await _natsJsContext.CreateStreamAsync(streamConfig, cancellationToken: token);

            logger.LogInformation("Stream '{Stream}' успешно инициализирован (Subject: {Subject}).",
                _settings.StreamName, _settings.SubjectName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при инициализации JetStream потока '{Stream}'.", _settings.StreamName);
            throw;
        }
    }

    /// <summary>
    /// Публикует пакет сообщений в NATS.
    /// </summary>
    /// <param name="rentals">Список DTO для публикации.</param>
    /// <param name="token">Токен отмены.</param>
    public async Task PublishBatchAsync(IEnumerable<RentalCreateUpdateDto> rentals, CancellationToken token = default)
    {
        var tasks = new List<Task>();
        var count = 0;

        foreach (var rental in rentals)
        {
            var task = _natsJsContext!.PublishAsync(_settings.SubjectName, rental, NatsJsonSerializer<RentalCreateUpdateDto>.Default, cancellationToken: token).AsTask();
            tasks.Add(task);
            count++;
        }

        try
        {
            await Task.WhenAll(tasks);

            logger.LogInformation("Успешно опубликовано {Count} событий аренды в тему '{Subject}'.",
                count, _settings.SubjectName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при публикации батча в NATS.");
            throw;
        }
    }
}