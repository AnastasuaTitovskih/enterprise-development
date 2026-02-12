using Microsoft.AspNetCore.Mvc;
using BikeRental.Application.Contracts;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Базовый контроллер, реализующий стандартные CRUD-операции.
/// </summary>
/// <typeparam name="TDto">DTO для чтения данных.</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для создания и обновления.</typeparam>
/// <param name="logger">Логгер.</param>
/// <param name="service">Сервис приложения.</param>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto>(
    ILogger logger,
    IApplicationService<TDto, TCreateUpdateDto, int> service) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
{
    /// <summary>
    /// Создает новую сущность.
    /// </summary>
    /// <param name="dto">Данные для создания.</param>
    /// <returns>Созданная сущность и код 201.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос на создание сущности {DtoType}", typeof(TCreateUpdateDto).Name);

            var createdEntity = await service.Create(dto);

            return CreatedAtAction(nameof(this.Create), createdEntity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании сущности");
            return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>DTO сущности.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Get(int id)
    {
        try
        {
            var entity = await service.Get(id);
            return Ok(entity);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Сущность с Id {Id} не найдена", id);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении сущности с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает список всех сущностей.
    /// </summary>
    /// <returns>Список DTO.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            var entities = await service.GetAll();
            return Ok(entities);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка сущностей");
            return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновляет существующую сущность.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="dto">Данные для обновления.</param>
    /// <returns>Обновленная сущность.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Update(int id, [FromBody] TCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос на обновление сущности с Id {Id}", id);
            var updatedEntity = await service.Update(dto, id);
            return Ok(updatedEntity);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось обновить: сущность с Id {Id} не найдена", id);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении сущности с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удаляет сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности.</param>
    /// <returns>Код 204 (No Content) при успехе.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос на удаление сущности с Id {Id}", id);
            var success = await service.Delete(id);

            if (!success)
            {
                logger.LogWarning("Не удалось удалить: сущность с Id {Id} не найдена", id);
                return NotFound(new { message = $"Сущность с Id {id} не найдена" });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении сущности с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера");
        }
    }
}