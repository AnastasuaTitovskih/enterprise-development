using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRental.Domain;

/// <summary>
/// Конкретный экземпляр велосипеда в пункте проката.
/// </summary>
public class Bike
{
    /// <summary>
    /// Уникальный идентификатор велосипеда.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Уникальный серийный номер.
    /// </summary>
    [MaxLength(50)]
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Цвет.
    /// </summary>
    [MaxLength(30)]
    public required string Color { get; set; }

    /// <summary>
    /// Идентификатор модели (Внешний ключ).
    /// </summary>
    public required int BikeModelId { get; set; }

    /// <summary>
    /// Навигационное свойство: Модель велосипеда.
    /// </summary>
    [ForeignKey(nameof(BikeModelId))]
    public virtual BikeModel? Model { get; set; }
}