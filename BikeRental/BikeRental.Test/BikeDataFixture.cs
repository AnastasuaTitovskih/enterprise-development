using BikeRental.Domain;
using BikeRental.Domain.Data;

namespace BikeRental.Test;

/// <summary>
/// Фикстура для тестов, предоставляющая доступ к тестовым данным.
/// </summary>
public class BikeDataFixture
{
    public List<BikeModel> Models => DataSeeder.BikeModels;
    public List<Bike> Bikes => DataSeeder.Bikes;
    public List<Renter> Renters => DataSeeder.Renters;
    public List<Rental> Rentals => DataSeeder.Rentals;
}