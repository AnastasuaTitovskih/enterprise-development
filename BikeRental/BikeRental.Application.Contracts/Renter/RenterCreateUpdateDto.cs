namespace BikeRental.Application.Contracts.Renter;

/// <summary>
/// DTO для создания или обновления данных арендатора.
/// </summary>
/// <param name="FullName">ФИО арендатора.</param>
/// <param name="PhoneNumber">Номер телефона.</param>
public record RenterCreateUpdateDto(
    string FullName,
    string PhoneNumber);