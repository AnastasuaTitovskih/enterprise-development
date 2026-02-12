using System.ComponentModel.DataAnnotations;

namespace BikeRental.Domain;

/// <summary>
/// Арендатор (клиент).
/// </summary>
public class Renter
{
    /// <summary>
    /// Уникальный идентификатор арендатора.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// ФИО арендатора.
    /// </summary>
    [MaxLength(150)]
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    [MaxLength(20)]
    [Phone]
    public required string PhoneNumber { get; set; }
}