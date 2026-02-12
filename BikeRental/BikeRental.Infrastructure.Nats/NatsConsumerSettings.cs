namespace BikeRental.Infrastructure.Nats;

/// <summary>
/// Настройки потребителя NATS JetStream.
/// </summary>
public class NatsConsumerSettings
{
    /// <summary>
    /// Имя потока (Stream), откуда читать сообщения.
    /// </summary>
    public required string StreamName { get; set; }

    /// <summary>
    /// Имя потребителя (Consumer) в JetStream.
    /// </summary>
    public required string ConsumerName { get; set; }

    /// <summary>
    /// Тема (Subject), на которую подписан потребитель.
    /// </summary>
    public required string SubjectName { get; set; }

    /// <summary>
    /// Размер пакета сообщений для одновременной обработки (по умолчанию 10).
    /// </summary>
    public int BatchSize { get; set; } = 10;

    /// <summary>
    /// Максимальное время ожидания заполнения батча (по умолчанию 5 сек).
    /// </summary>
    public int BatchTimeoutSeconds { get; set; } = 5;
}