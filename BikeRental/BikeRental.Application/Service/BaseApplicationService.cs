using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Domain;

namespace BikeRental.Application.Service;

/// <summary>
/// Базовая реализация прикладного сервиса для CRUD-операций.
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности.</typeparam>
/// <typeparam name="TDto">Тип DTO для чтения.</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания/обновления.</typeparam>
/// <param name="repository">Репозиторий сущности.</param>
/// <param name="mapper">Маппер.</param>
public abstract class BaseApplicationService<TEntity, TDto, TCreateUpdateDto>(
    IRepository<TEntity, int> repository,
    IMapper mapper)
    : IApplicationService<TDto, TCreateUpdateDto, int>
    where TEntity : class
    where TDto : class
    where TCreateUpdateDto : class
{
    protected readonly IRepository<TEntity, int> Repository = repository;
    protected readonly IMapper Mapper = mapper;

    /// <inheritdoc/>
    public virtual async Task<TDto> Create(TCreateUpdateDto dto)
    {
        var entity = Mapper.Map<TEntity>(dto);
        var createdEntity = await Repository.Create(entity);
        return Mapper.Map<TDto>(createdEntity);
    }

    /// <inheritdoc/>
    public virtual async Task<TDto> Get(int id)
    {
        var entity = await Repository.Get(id) 
            ?? throw new KeyNotFoundException($"Сущность с Id: {id} не найдена");
        return Mapper.Map<TDto>(entity);
    }

    /// <inheritdoc/>
    public virtual async Task<IList<TDto>> GetAll()
    {
        var entities = await Repository.GetAll();
        return Mapper.Map<IList<TDto>>(entities);
    }

    /// <inheritdoc/>
    public virtual async Task<TDto> Update(TCreateUpdateDto dto, int id)
    {
        var existingEntity = await Repository.Get(id)
            ?? throw new KeyNotFoundException($"Сущность с Id: {id} не найдена");

        Mapper.Map(dto, existingEntity);

        var updatedEntity = await Repository.Update(existingEntity);
        return Mapper.Map<TDto>(updatedEntity);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> Delete(int id)
    {
        return await Repository.Delete(id);
    }
}