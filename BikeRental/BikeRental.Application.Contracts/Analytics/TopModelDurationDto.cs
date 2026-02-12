namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO статистики модели по длительности аренды.
/// </summary>
/// <param name="ModelName">Название модели.</param>
/// <param name="TotalDurationHours">Общая длительность аренды в часах.</param>
public record TopModelDurationDto(
    string ModelName,
    int TotalDurationHours);