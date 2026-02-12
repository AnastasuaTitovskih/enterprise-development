using BikeRental.Generator.Nats.Host.Configuration;
using BikeRental.Generator.Nats.Host.Generator;
using BikeRental.Generator.Nats.Host.Producer;
using Microsoft.Extensions.Options;

namespace BikeRental.Generator.Nats.Host.Worker;

/// <summary>
/// Фоновый сервис, управляющий процессом генерации и отправки тестовых данных.
/// </summary>
/// <param name="generator">Генератор данных.</param>
/// <param name="producer">Продюсер NATS JetStream.</param>
/// <param name="settings">Настройки генерации (интервал, размер батча).</param>
/// <param name="logger">Логгер.</param>
public class RentalGeneratorWorker(
    RentalGenerator generator,
    RentalProducer producer,
    IOptions<GeneratorSettings> settings,
    ILogger<RentalGeneratorWorker> logger) : BackgroundService
{
    private readonly GeneratorSettings _settings = settings.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Запуск RentalGeneratorWorker...");

        try
        {
            await producer.EnsureStreamCreatedAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Не удалось инициализировать NATS Stream. Сервис генерации остановлен.");
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogDebug("Генерация батча из {Count} элементов...", _settings.BatchSize);

                var batch = generator.Generate(_settings.BatchSize);

                await producer.PublishBatchAsync(batch, stoppingToken);

                logger.LogInformation("Цикл завершен. Ожидание {Interval} мс.", _settings.GenerationIntervalMs);

                await Task.Delay(_settings.GenerationIntervalMs, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка в цикле генерации данных. Ожидание 5 секунд перед повторной попыткой.");

                try
                {
                    await Task.Delay(5000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    logger.LogWarning("Генерация записей об аренде отменена, сервис RentalGeneratorWorker останавливается.");
                    break;
                }
            }
        }

        logger.LogInformation("RentalGeneratorWorker остановлен.");
    }
}