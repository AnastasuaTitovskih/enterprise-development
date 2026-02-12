using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRental.Domain;

/// <summary>
/// Справочник моделей велосипедов.
/// Содержит технические характеристики и тарифы.
/// </summary>
public class BikeModel
{
    /// <summary>
    /// Уникальный идентификатор модели.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Название модели.
    /// </summary>
    [MaxLength(100)]
    public required string Name { get; set; }

    /// <summary>
    /// Тип велосипеда.
    /// </summary>
    public required BikeType Type { get; set; }

    /// <summary>
    /// Размер колес в дюймах.
    /// </summary>
    public required double WheelSizeInches { get; set; }

    /// <summary>
    /// Максимальный вес пассажира (кг).
    /// </summary>
    public required double MaxPassengerWeightKg { get; set; }

    /// <summary>
    /// Вес велосипеда (кг).
    /// </summary>
    public required double BikeWeightKg { get; set; }

    /// <summary>
    /// Тип тормозной системы.
    /// </summary>
    [MaxLength(50)]
    public required string BrakeType { get; set; }

    /// <summary>
    /// Год выпуска модели.
    /// </summary>
    public required int ModelYear { get; set; }

    /// <summary>
    /// Стоимость аренды за один час.
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public required decimal HourlyPrice { get; set; }
}