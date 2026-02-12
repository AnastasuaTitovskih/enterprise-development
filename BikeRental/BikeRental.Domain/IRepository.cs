namespace BikeRental.Domain;

/// <summary>
/// Определяет универсальный интерфейс репозитория,
/// предоставляющий базовые операции CRUD (создание, чтение, обновление, удаление)
/// </summary>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Добавляет новую сущность в репозиторий
    /// </summary>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Получает сущность по её идентификатору
    /// </summary>
    public Task<TEntity?> Get(TKey entityId);

    /// <summary>
    /// Возвращает список всех сущностей в репозитории
    /// </summary>
    public Task<IList<TEntity>> GetAll();

    /// <summary>
    /// Обновляет данные существующей сущности
    /// </summary>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удаляет сущность по её идентификатору
    /// </summary>
    public Task<bool> Delete(TKey entityId);
}