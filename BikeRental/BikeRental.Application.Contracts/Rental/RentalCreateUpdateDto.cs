namespace BikeRental.Application.Contracts.Rental;

/// <summary>
/// DTO для регистрации факта аренды.
/// </summary>
/// <param name="BikeId">Идентификатор велосипеда.</param>
/// <param name="RenterId">Идентификатор арендатора.</param>
/// <param name="RentStart">Время начала аренды.</param>
/// <param name="DurationHours">Продолжительность аренды (в часах).</param>
public record RentalCreateUpdateDto(
    int BikeId,
    int RenterId,
    DateTimeOffset RentStart,
    int DurationHours);