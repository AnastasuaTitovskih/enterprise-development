namespace BikeRental.Generator.Nats.Host.Configuration;

/// <summary>
/// Настройки генератора тестовых данных аренды.
/// </summary>
public class GeneratorSettings
{
    /// <summary>
    /// Интервал генерации новой пачки данных в миллисекундах.
    /// </summary>
    public int GenerationIntervalMs { get; set; } = 5000;

    /// <summary>
    /// Количество элементов, генерируемых за один цикл.
    /// </summary>
    public int BatchSize { get; set; } = 5;

    /// <summary>
    /// Минимальный Id велосипеда.
    /// </summary>
    public int MinBikeId { get; set; } = 1;

    /// <summary>
    /// Максимальный Id велосипеда.
    /// </summary>
    public int MaxBikeId { get; set; } = 15;

    /// <summary>
    /// Минимальный Id арендатора.
    /// </summary>
    public int MinRenterId { get; set; } = 1;

    /// <summary>
    /// Максимальный Id арендатора.
    /// </summary>
    public int MaxRenterId { get; set; } = 15;

    /// <summary>
    /// Минимальная длительность аренды в часах.
    /// </summary>
    public int MinDurationHours { get; set; } = 1;

    /// <summary>
    /// Максимальная длительность аренды в часах.
    /// </summary>
    public int MaxDurationHours { get; set; } = 24;

    /// <summary>
    /// Глубина генерации даты начала аренды в днях (сколько дней назад от текущего момента).
    /// </summary>
    public int RentStartDaysBack { get; set; } = 30;
}