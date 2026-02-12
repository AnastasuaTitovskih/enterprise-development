namespace BikeRental.Application.Contracts.Renter;

/// <summary>
/// DTO для отображения данных арендатора.
/// </summary>
/// <param name="Id">Уникальный идентификатор арендатора.</param>
/// <param name="FullName">ФИО арендатора.</param>
/// <param name="PhoneNumber">Номер телефона.</param>
public record RenterDto(
    int Id,
    string FullName,
    string PhoneNumber);