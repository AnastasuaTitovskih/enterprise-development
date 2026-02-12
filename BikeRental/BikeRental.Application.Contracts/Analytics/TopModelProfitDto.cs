namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO статистики модели по прибыли.
/// </summary>
/// <param name="ModelName">Название модели.</param>
/// <param name="TotalProfit">Общая прибыль за все время.</param>
public record TopModelProfitDto(
    string ModelName,
    decimal TotalProfit);