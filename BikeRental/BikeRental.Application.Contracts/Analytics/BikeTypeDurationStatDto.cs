using BikeRental.Domain.Enum;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO статистики времени по типу велосипеда.
/// </summary>
/// <param name="Type">Тип велосипеда.</param>
/// <param name="TotalDurationHours">Суммарное время аренды.</param>
public record BikeTypeDurationStatDto(
    BikeType Type,
    int TotalDurationHours);