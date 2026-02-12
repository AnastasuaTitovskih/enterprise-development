using BikeRental.Domain.Data;
using BikeRental.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных для пункта проката велосипедов.
/// </summary>
/// <param name="options">Настройки контекста.</param>
public class BikeRentalDbContext(DbContextOptions<BikeRentalDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица моделей велосипедов.
    /// </summary>
    public DbSet<BikeModel> BikeModels { get; set; }

    /// <summary>
    /// Таблица конкретных велосипедов.
    /// </summary>
    public DbSet<Bike> Bikes { get; set; }

    /// <summary>
    /// Таблица арендаторов.
    /// </summary>
    public DbSet<Renter> Renters { get; set; }

    /// <summary>
    /// Таблица записей об аренде.
    /// </summary>
    public DbSet<Rental> Rentals { get; set; }

    /// <summary>
    /// Настройка модели при создании.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BikeModel>(entity =>
        {
            entity.Property(e => e.Type)
                  .HasConversion<string>();
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.Property(e => e.RentStart)
                  .HasColumnType("timestamp with time zone");
        });

        modelBuilder.Entity<BikeModel>().HasData(DataSeeder.BikeModels);
        modelBuilder.Entity<Bike>().HasData(DataSeeder.Bikes);
        modelBuilder.Entity<Renter>().HasData(DataSeeder.Renters);
        modelBuilder.Entity<Rental>().HasData(DataSeeder.Rentals);
    }
}