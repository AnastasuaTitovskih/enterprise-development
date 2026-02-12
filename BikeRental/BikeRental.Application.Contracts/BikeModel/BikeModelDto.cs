using BikeRental.Domain.Enum;

namespace BikeRental.Application.Contracts.BikeModel;

/// <summary>
/// DTO для отображения модели велосипеда.
/// </summary>
/// <param name="Id">Уникальный идентификатор модели.</param>
/// <param name="Name">Название модели.</param>
/// <param name="Type">Тип велосипеда.</param>
/// <param name="WheelSizeInches">Размер колес в дюймах.</param>
/// <param name="MaxPassengerWeightKg">Максимальный вес пассажира (кг).</param>
/// <param name="BikeWeightKg">Вес самого велосипеда (кг).</param>
/// <param name="BrakeType">Тип тормозной системы.</param>
/// <param name="ModelYear">Год выпуска модели.</param>
/// <param name="HourlyPrice">Стоимость аренды в час.</param>
public record BikeModelDto(
    int Id,
    string Name,
    BikeType Type,
    double WheelSizeInches,
    double MaxPassengerWeightKg,
    double BikeWeightKg,
    string BrakeType,
    int ModelYear,
    decimal HourlyPrice);