using BikeRental.Application.Contracts.Renter;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO активности клиента.
/// </summary>
/// <param name="Renter">Полная информация об арендаторе (DTO).</param>
/// <param name="RentalCount">Количество совершенных аренд.</param>
public record RenterActivityDto(
    RenterDto Renter,
    int RentalCount);