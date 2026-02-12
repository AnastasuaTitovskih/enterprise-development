using BikeRental.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repository;

/// <summary>
/// Репозиторий для управления записями об аренде.
/// Обеспечивает загрузку полной информации об аренде, включая клиента и велосипед.
/// </summary>
/// <param name="context">Контекст базы данных.</param>
public class RentalRepository(BikeRentalDbContext context) : BaseRepository<Rental>(context)
{
    /// <summary>
    /// Получает запись аренды по Id.
    /// Подгружает Арендатора, велосипед и модель велосипеда.
    /// </summary>
    /// <param name="id">Идентификатор аренды.</param>
    /// <returns>Полная информация об аренде.</returns>
    public override async Task<Rental?> Get(int id)
    {
        return await DbSet
            .Include(r => r.Renter)
            .Include(r => r.Bike)
                .ThenInclude(b => b!.Model)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Получает все записи об аренде.
    /// Подгружает Арендатора, велосипед и модель велосипеда.
    /// </summary>
    /// <returns>Список всех аренд с подробностями.</returns>
    public override async Task<IList<Rental>> GetAll()
    {
        return await DbSet
            .Include(r => r.Renter)
            .Include(r => r.Bike)
                .ThenInclude(b => b!.Model)
            .ToListAsync();
    }
}