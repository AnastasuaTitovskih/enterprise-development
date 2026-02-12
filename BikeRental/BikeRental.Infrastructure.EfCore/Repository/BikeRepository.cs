using BikeRental.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repository;

/// <summary>
/// Репозиторий для управления велосипедами.
/// Переопределяет методы чтения для загрузки связанных данных.
/// </summary>
/// <param name="context">Контекст базы данных.</param>
public class BikeRepository(BikeRentalDbContext context) : BaseRepository<Bike>(context)
{
    /// <summary>
    /// Получает велосипед по Id с подгруженной моделью.
    /// </summary>
    /// <param name="id">Идентификатор велосипеда.</param>
    /// <returns>Велосипед с данными о модели.</returns>
    public override async Task<Bike?> Get(int id)
    {
        return await DbSet
            .Include(b => b.Model)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    /// <summary>
    /// Получает все велосипеды с подгруженными моделями.
    /// </summary>
    /// <returns>Список велосипедов с моделями.</returns>
    public override async Task<IList<Bike>> GetAll()
    {
        return await DbSet
            .Include(b => b.Model)
            .ToListAsync();
    }
}