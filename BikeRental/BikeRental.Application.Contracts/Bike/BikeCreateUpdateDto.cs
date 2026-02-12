namespace BikeRental.Application.Contracts.Bike;

/// <summary>
/// DTO для создания или обновления велосипеда.
/// </summary>
/// <param name="SerialNumber">Серийный номер рамы.</param>
/// <param name="Color">Цвет велосипеда.</param>
/// <param name="BikeModelId">Идентификатор модели велосипеда.</param>
public record BikeCreateUpdateDto(
    string SerialNumber,
    string Color,
    int BikeModelId);