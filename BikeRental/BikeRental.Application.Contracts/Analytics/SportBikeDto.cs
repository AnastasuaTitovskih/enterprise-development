using BikeRental.Domain.Enum;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO для отображения информации о спортивном велосипеде.
/// </summary>
/// <param name="Id">Id велосипеда.</param>
/// <param name="SerialNumber">Серийный номер.</param>
/// <param name="ModelName">Название модели.</param>
/// <param name="Type">Тип (Mountain/Road).</param>
/// <param name="HourlyPrice">Цена за час.</param>
public record SportBikeDto(
    int Id,
    string SerialNumber,
    string ModelName,
    BikeType Type,
    decimal HourlyPrice);