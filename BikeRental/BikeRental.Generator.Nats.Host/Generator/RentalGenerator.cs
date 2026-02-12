using BikeRental.Application.Contracts.Rental;
using BikeRental.Generator.Nats.Host.Configuration;
using Bogus;
using Microsoft.Extensions.Options;

namespace BikeRental.Generator.Nats.Host.Generator;

/// <summary>
/// Генератор фейковых данных для сущности Rental.
/// Использует библиотеку Bogus.
/// </summary>
/// <param name="settings">Настройки генерации (границы Id, даты и т.д.).</param>
public class RentalGenerator(IOptions<GeneratorSettings> settings)
{
    private readonly Faker<RentalCreateUpdateDto> _faker = CreateFaker(settings.Value);

    /// <summary>
    /// Создает и настраивает экземпляр Faker с правилами генерации.
    /// </summary>
    /// <param name="settings">Настройки с границами значений.</param>
    /// <returns>Настроенный Faker.</returns>
    private static Faker<RentalCreateUpdateDto> CreateFaker(GeneratorSettings settings) =>
        new Faker<RentalCreateUpdateDto>()
            .CustomInstantiator(f => new RentalCreateUpdateDto(
                BikeId: f.Random.Int(settings.MinBikeId, settings.MaxBikeId),
                RenterId: f.Random.Int(settings.MinRenterId, settings.MaxRenterId),
                RentStart: f.Date.PastOffset(settings.RentStartDaysBack).ToUniversalTime(),
                DurationHours: f.Random.Int(settings.MinDurationHours, settings.MaxDurationHours)
            ));

    /// <summary>
    /// Генерирует указанное количество записей аренды.
    /// </summary>
    /// <param name="count">Количество записей.</param>
    /// <returns>Коллекция сгенерированных DTO.</returns>
    public IEnumerable<RentalCreateUpdateDto> Generate(int count) => _faker.Generate(count);
}