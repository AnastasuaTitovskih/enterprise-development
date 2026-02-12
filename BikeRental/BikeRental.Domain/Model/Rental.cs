using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRental.Domain.Model;

/// <summary>
/// Факт аренды транспортного средства.
/// </summary>
public class Rental
{
    /// <summary>
    /// Уникальный идентификатор записи аренды.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор выданного велосипеда (Внешний ключ).
    /// </summary>
    public required int BikeId { get; set; }

    /// <summary>
    /// Навигационное свойство: Велосипед.
    /// </summary>
    [ForeignKey(nameof(BikeId))]
    public virtual Bike? Bike { get; set; }

    /// <summary>
    /// Идентификатор арендатора (Внешний ключ).
    /// </summary>
    public required int RenterId { get; set; }

    /// <summary>
    /// Навигационное свойство: Арендатор.
    /// </summary>
    [ForeignKey(nameof(RenterId))]
    public virtual Renter? Renter { get; set; }

    /// <summary>
    /// Время начала аренды (UTC).
    /// </summary>
    public required DateTimeOffset RentStart { get; set; }

    /// <summary>
    /// Зафиксированная продолжительность аренды в часах.
    /// </summary>
    public required int DurationHours { get; set; }
}