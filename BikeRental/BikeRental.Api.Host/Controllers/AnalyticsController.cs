using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для получения аналитических отчетов и статистики.
/// </summary>
/// <param name="logger">Логгер.</param>
/// <param name="analyticsService">Сервис аналитики.</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    ILogger<AnalyticsController> logger,
    IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Получает список всех спортивных велосипедов (Mountain и Road).
    /// </summary>
    /// <returns>Список спортивных велосипедов.</returns>
    [HttpGet("sport-bikes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<SportBikeDto>>> GetSportBikes()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: Спортивные велосипеды");
            var result = await analyticsService.GetSportBikes();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка спортивных велосипедов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает топ моделей по прибыли.
    /// </summary>
    /// <param name="count">Количество записей (по умолчанию 5).</param>
    /// <returns>Список моделей с прибылью.</returns>
    [HttpGet("top-models/profit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TopModelProfitDto>>> GetTopModelsByProfit([FromQuery] int count = 5)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: Топ {Count} моделей по прибыли", count);
            var result = await analyticsService.GetTopModelsByProfit(count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топа моделей по прибыли");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает топ моделей по длительности аренды.
    /// </summary>
    /// <param name="count">Количество записей (по умолчанию 5).</param>
    /// <returns>Список моделей с длительностью.</returns>
    [HttpGet("top-models/duration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TopModelDurationDto>>> GetTopModelsByDuration([FromQuery] int count = 5)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: Топ {Count} моделей по длительности", count);
            var result = await analyticsService.GetTopModelsByDuration(count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топа моделей по длительности");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает общую статистику по времени аренды (мин, макс, среднее).
    /// </summary>
    /// <returns>Объект статистики.</returns>
    [HttpGet("rental-stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDurationStatsDto>> GetRentalDurationStats()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: Общая статистика времени аренды");
            var result = await analyticsService.GetRentalDurationStats();

            if (result == null)
            {
                return NotFound("Нет данных об арендах для расчета статистики");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при расчете статистики аренды");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает суммарное время аренды по типам велосипедов.
    /// </summary>
    /// <returns>Список статистики по типам.</returns>
    [HttpGet("bike-type-duration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<BikeTypeDurationStatDto>>> GetDurationByBikeType()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: Время аренды по типам");
            var result = await analyticsService.GetDurationByBikeType();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении статистики по типам велосипедов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает топ самых активных клиентов.
    /// </summary>
    /// <param name="count">Количество записей (по умолчанию 5).</param>
    /// <returns>Список клиентов с количеством аренд.</returns>
    [HttpGet("top-renters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<RenterActivityDto>>> GetTopRenters([FromQuery] int count = 5)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: Топ {Count} клиентов", count);
            var result = await analyticsService.GetTopRenters(count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топа клиентов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}