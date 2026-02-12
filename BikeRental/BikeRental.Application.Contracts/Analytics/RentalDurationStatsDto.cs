namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO общей статистики времени аренды.
/// </summary>
/// <param name="MinDurationHours">Минимальное время аренды.</param>
/// <param name="MaxDurationHours">Максимальное время аренды.</param>
/// <param name="AverageDurationHours">Среднее время аренды.</param>
public record RentalDurationStatsDto(
    int MinDurationHours,
    int MaxDurationHours,
    double AverageDurationHours);