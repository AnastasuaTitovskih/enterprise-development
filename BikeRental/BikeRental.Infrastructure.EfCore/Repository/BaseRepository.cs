using Microsoft.EntityFrameworkCore;
using BikeRental.Domain;

namespace BikeRental.Infrastructure.EfCore.Repository;

/// <summary>
/// Базовая абстрактная реализация репозитория с использованием EF Core.
/// Реализует основные CRUD-операции.
/// </summary>
/// <typeparam name="TEntity">Тип сущности, хранящейся в репозитории.</typeparam>
/// <param name="context">Контекст базы данных.</param>
public abstract class BaseRepository<TEntity>(BikeRentalDbContext context) : IRepository<TEntity, int>
    where TEntity : class
{
    /// <summary>
    /// Набор данных для текущей сущности.
    /// </summary>
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    /// <summary>
    /// Асинхронно создает новую сущность в базе данных.
    /// </summary>
    /// <param name="entity">Сущность для создания.</param>
    /// <returns>Созданная сущность.</returns>
    public virtual async Task<TEntity> Create(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Асинхронно получает сущность по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>Найденная сущность или null, если она не найдена.</returns>
    public virtual async Task<TEntity?> Get(int id)
    {
        return await DbSet.FindAsync(id);
    }

    /// <summary>
    /// Асинхронно получает список всех сущностей.
    /// </summary>
    /// <returns>Список сущностей.</returns>
    public virtual async Task<IList<TEntity>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    /// <summary>
    /// Асинхронно обновляет данные сущности.
    /// </summary>
    /// <param name="entity">Сущность с обновленными данными.</param>
    /// <returns>Обновленная сущность.</returns>
    public virtual async Task<TEntity> Update(TEntity entity)
    {
        DbSet.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Асинхронно удаляет сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности.</param>
    /// <returns>True, если удаление прошло успешно; иначе False.</returns>
    public virtual async Task<bool> Delete(int id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        DbSet.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}