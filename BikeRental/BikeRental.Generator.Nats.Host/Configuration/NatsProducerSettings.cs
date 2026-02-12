namespace BikeRental.Generator.Nats.Host.Configuration;

/// <summary>
/// Настройки подключения к NATS для продюсера.
/// </summary>
public class NatsProducerSettings
{
    /// <summary>
    /// Имя потока в JetStream.
    /// </summary>
    public required string StreamName { get; set; } = "RENTALS";

    /// <summary>
    /// Тема, в которую будут публиковаться сообщения.
    /// </summary>
    public required string SubjectName { get; set; } = "rental.create";
}