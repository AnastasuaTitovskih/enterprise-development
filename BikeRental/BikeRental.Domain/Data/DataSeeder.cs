using BikeRental.Domain.Enum;
using BikeRental.Domain.Model;

namespace BikeRental.Domain.Data;

/// <summary>
/// Класс для инициализации тестовых данных.
/// Содержит списки сущностей, связанных по Id.
/// </summary>
public static class DataSeeder
{
    private static readonly DateTimeOffset _baseDate = new(2026, 2, 12, 12, 0, 0, TimeSpan.Zero);

    /// <summary>
    /// Тестовый список моделей велосипедов (10 шт).
    /// </summary>
    public static readonly List<BikeModel> BikeModels =
    [
        new()
        {
            Id = 1,
            Name = "Mountain King 2000",
            Type = BikeType.Mountain,
            WheelSizeInches = 29.0,
            MaxPassengerWeightKg = 120.0,
            BikeWeightKg = 14.5,
            BrakeType = "Hydraulic Disc",
            ModelYear = 2023,
            HourlyPrice = 500.00m
        },
        new()
        {
            Id = 2,
            Name = "City Glider",
            Type = BikeType.City,
            WheelSizeInches = 28.0,
            MaxPassengerWeightKg = 110.0,
            BikeWeightKg = 16.0,
            BrakeType = "V-Brake",
            ModelYear = 2022,
            HourlyPrice = 300.00m
        },
        new()
        {
            Id = 3,
            Name = "Speed Demon",
            Type = BikeType.Road,
            WheelSizeInches = 28.0,
            MaxPassengerWeightKg = 100.0,
            BikeWeightKg = 9.5,
            BrakeType = "Mechanical Disc",
            ModelYear = 2024,
            HourlyPrice = 600.00m
        },
        new()
        {
            Id = 4,
            Name = "Electro Bolt",
            Type = BikeType.Electric,
            WheelSizeInches = 27.5,
            MaxPassengerWeightKg = 130.0,
            BikeWeightKg = 22.0,
            BrakeType = "Hydraulic Disc",
            ModelYear = 2023,
            HourlyPrice = 900.00m
        },
        new()
        {
            Id = 5,
            Name = "Little Rider",
            Type = BikeType.Children,
            WheelSizeInches = 20.0,
            MaxPassengerWeightKg = 50.0,
            BikeWeightKg = 8.0,
            BrakeType = "Coaster",
            ModelYear = 2021,
            HourlyPrice = 150.00m
        },
        new()
        {
            Id = 6,
            Name = "Trail Master X",
            Type = BikeType.Mountain,
            WheelSizeInches = 27.5,
            MaxPassengerWeightKg = 125.0,
            BikeWeightKg = 15.0,
            BrakeType = "Hydraulic Disc",
            ModelYear = 2022,
            HourlyPrice = 450.00m
        },
        new()
        {
            Id = 7,
            Name = "Urban Commuter",
            Type = BikeType.City,
            WheelSizeInches = 28.0,
            MaxPassengerWeightKg = 115.0,
            BikeWeightKg = 15.5,
            BrakeType = "Rim",
            ModelYear = 2020,
            HourlyPrice = 250.00m
        },
        new()
        {
            Id = 8,
            Name = "Aero Carbon",
            Type = BikeType.Road,
            WheelSizeInches = 28.0,
            MaxPassengerWeightKg = 95.0,
            BikeWeightKg = 8.2,
            BrakeType = "Rim",
            ModelYear = 2023,
            HourlyPrice = 700.00m
        },
        new()
        {
            Id = 9,
            Name = "Volt City",
            Type = BikeType.Electric,
            WheelSizeInches = 26.0,
            MaxPassengerWeightKg = 120.0,
            BikeWeightKg = 24.0,
            BrakeType = "Mechanical Disc",
            ModelYear = 2022,
            HourlyPrice = 850.00m
        },
        new()
        {
            Id = 10,
            Name = "Junior Racer",
            Type = BikeType.Children,
            WheelSizeInches = 24.0,
            MaxPassengerWeightKg = 60.0,
            BikeWeightKg = 10.0,
            BrakeType = "V-Brake",
            ModelYear = 2023,
            HourlyPrice = 200.00m
        }
    ];

    /// <summary>
    /// Тестовый список велосипедов (10 шт).
    /// Ссылаются на модели через BikeModelId.
    /// </summary>
    public static readonly List<Bike> Bikes =
    [
        new() { Id = 1, SerialNumber = "SN-MTB-001", Color = "Black", BikeModelId = 1 },
        new() { Id = 2, SerialNumber = "SN-CTY-002", Color = "White", BikeModelId = 2 },
        new() { Id = 3, SerialNumber = "SN-RD-003", Color = "Red", BikeModelId = 3 },
        new() { Id = 4, SerialNumber = "SN-EL-004", Color = "Blue", BikeModelId = 4 },
        new() { Id = 5, SerialNumber = "SN-CHD-005", Color = "Pink", BikeModelId = 5 },
        new() { Id = 6, SerialNumber = "SN-MTB-006", Color = "Green", BikeModelId = 6 },
        new() { Id = 7, SerialNumber = "SN-CTY-007", Color = "Grey", BikeModelId = 7 },
        new() { Id = 8, SerialNumber = "SN-RD-008", Color = "Yellow", BikeModelId = 8 },
        new() { Id = 9, SerialNumber = "SN-EL-009", Color = "Matte Black", BikeModelId = 9 },
        new() { Id = 10, SerialNumber = "SN-CHD-010", Color = "Orange", BikeModelId = 10 }
    ];

    /// <summary>
    /// Тестовый список арендаторов (10 шт).
    /// </summary>
    public static readonly List<Renter> Renters =
    [
        new() { Id = 1, FullName = "Иванов Иван Иванович", PhoneNumber = "+79001112233" },
        new() { Id = 2, FullName = "Петров Петр Петрович", PhoneNumber = "+79002223344" },
        new() { Id = 3, FullName = "Сидоров Владимир Викторович", PhoneNumber = "+79003334455" },
        new() { Id = 4, FullName = "Кузнецова Анна Сергеевна", PhoneNumber = "+79004445566" },
        new() { Id = 5, FullName = "Смирнов Алексей Дмитриевич", PhoneNumber = "+79005556677" },
        new() { Id = 6, FullName = "Попова Мария Ивановна", PhoneNumber = "+79006667788" },
        new() { Id = 7, FullName = "Васильев Дмитрий Олегович", PhoneNumber = "+79007778899" },
        new() { Id = 8, FullName = "Соколов Сергей Павлович", PhoneNumber = "+79008889900" },
        new() { Id = 9, FullName = "Михайлова Елена Владимировна", PhoneNumber = "+79009990011" },
        new() { Id = 10, FullName = "Новиков Артем Игоревич", PhoneNumber = "+79000001122" }
    ];

    /// <summary>
    /// Тестовый список аренд (10 шт).
    /// Ссылаются на BikeId и RenterId.
    /// </summary>
    public static readonly List<Rental> Rentals =
    [
        new()
        {
            Id = 1,
            BikeId = 1,
            RenterId = 1,
            RentStart = _baseDate.AddDays(-10),
            DurationHours = 2
        },
        new()
        {
            Id = 2,
            BikeId = 3,
            RenterId = 1,
            RentStart = _baseDate.AddDays(-8),
            DurationHours = 5
        },
        new()
        {
            Id = 3,
            BikeId = 2,
            RenterId = 2,
            RentStart = _baseDate.AddDays(-7),
            DurationHours = 1
        },
        new()
        {
            Id = 4,
            BikeId = 4,
            RenterId = 2,
            RentStart = _baseDate.AddDays(-6),
            DurationHours = 3
        },
        new()
        {
            Id = 5,
            BikeId = 1,
            RenterId = 3,
            RentStart = _baseDate.AddDays(-5),
            DurationHours = 4
        },
        new()
        {
            Id = 6,
            BikeId = 4,
            RenterId = 4,
            RentStart = _baseDate.AddDays(-2),
            DurationHours = 24
        },
        new()
        {
            Id = 7,
            BikeId = 5,
            RenterId = 5,
            RentStart = _baseDate.AddDays(-1),
            DurationHours = 1
        },
        new()
        {
            Id = 8,
            BikeId = 2,
            RenterId = 1,
            RentStart = _baseDate.AddHours(-10),
            DurationHours = 2
        },
        new()
        {
            Id = 9,
            BikeId = 6,
            RenterId = 6,
            RentStart = _baseDate.AddHours(-5),
            DurationHours = 10
        },
        new()
        {
            Id = 10,
            BikeId = 7,
            RenterId = 7,
            RentStart = _baseDate.AddHours(-2),
            DurationHours = 1
        }
    ];
}