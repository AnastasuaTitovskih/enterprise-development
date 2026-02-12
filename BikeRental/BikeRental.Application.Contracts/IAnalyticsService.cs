using BikeRental.Application.Contracts.Analytics;

namespace BikeRental.Application.Contracts;

/// <summary>
/// Интерфейс сервиса аналитики.
/// Предоставляет методы для получения агрегированных данных и отчетов.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получает список всех спортивных велосипедов (Mountain и Road).
    /// </summary>
    /// <returns>Список спортивных велосипедов с данными о модели.</returns>
    public Task<IList<SportBikeDto>> GetSportBikes();

    /// <summary>
    /// Получает топ моделей велосипедов по общей прибыли.
    /// </summary>
    /// <param name="count">Количество позиций в топе (по умолчанию 5).</param>
    /// <returns>Список моделей с суммарной прибылью.</returns>
    public Task<IList<TopModelProfitDto>> GetTopModelsByProfit(int count = 5);

    /// <summary>
    /// Получает топ моделей велосипедов по общей длительности аренды.
    /// </summary>
    /// <param name="count">Количество позиций в топе (по умолчанию 5).</param>
    /// <returns>Список моделей с суммарной длительностью.</returns>
    public Task<IList<TopModelDurationDto>> GetTopModelsByDuration(int count = 5);

    /// <summary>
    /// Получает общую статистику по длительности аренд (мин, макс, среднее).
    /// </summary>
    /// <returns>Объект статистики или null, если данных нет.</returns>
    public Task<RentalDurationStatsDto?> GetRentalDurationStats();

    /// <summary>
    /// Получает суммарное время аренды по типам велосипедов.
    /// </summary>
    /// <returns>Список статистики по типам.</returns>
    public Task<IList<BikeTypeDurationStatDto>> GetDurationByBikeType();

    /// <summary>
    /// Получает список самых активных клиентов.
    /// </summary>
    /// <param name="count">Количество клиентов (по умолчанию 5).</param>
    /// <returns>Список DTO, содержащих данные клиента и количество его аренд.</returns>
    public Task<IList<RenterActivityDto>> GetTopRenters(int count = 5);
}