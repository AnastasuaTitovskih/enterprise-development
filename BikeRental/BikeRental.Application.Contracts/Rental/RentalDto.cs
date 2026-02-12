namespace BikeRental.Application.Contracts.Rental;

/// <summary>
/// DTO для отображения факта аренды.
/// </summary>
/// <param name="Id">Уникальный идентификатор записи аренды.</param>
/// <param name="BikeId">Идентификатор велосипеда.</param>
/// <param name="RenterId">Идентификатор арендатора.</param>
/// <param name="RentStart">Время начала аренды.</param>
/// <param name="DurationHours">Продолжительность аренды (в часах).</param>
public record RentalDto(
    int Id,
    int BikeId,
    int RenterId,
    DateTimeOffset RentStart,
    int DurationHours);