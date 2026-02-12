namespace BikeRental.Application.Contracts.Bike;

/// <summary>
/// DTO для отображения велосипеда.
/// </summary>
/// <param name="Id">Уникальный идентификатор велосипеда.</param>
/// <param name="SerialNumber">Серийный номер рамы.</param>
/// <param name="Color">Цвет велосипеда.</param>
/// <param name="BikeModelId">Идентификатор модели велосипеда.</param>
public record BikeDto(
    int Id,
    string SerialNumber,
    string Color,
    int BikeModelId);