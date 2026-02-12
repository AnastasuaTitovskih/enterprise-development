using BikeRental.Domain;

namespace BikeRental.Test;

/// <summary>
/// Тесты бизнес-логики проката велосипедов.
/// </summary>
public class BikeRentalTests(BikeDataFixture fixture) : IClassFixture<BikeDataFixture>
{
    /// <summary>
    /// Вывести информацию обо всех спортивных велосипедах.
    /// </summary>
    [Fact]
    public void GetSportBikes_WhenFilteringByMountainAndRoad_ReturnsFourSpecificBikes()
    {
        var sportTypes = new[] { BikeType.Mountain, BikeType.Road };

        var sportBikes = fixture.Bikes
            .Where(b => fixture.Models.Any(m => m.Id == b.BikeModelId && sportTypes.Contains(m.Type)))
            .ToList();

        Assert.Equal(4, sportBikes.Count);
    }

    /// <summary>
    /// Вывести топ моделей по прибыли и длительности.
    /// </summary>
    [Fact]
    public void GetTopModels_WhenSortedByProfitAndDuration_ReturnsElectroBoltAsLeader()
    {
        var modelStats = fixture.Rentals
            .Join(fixture.Bikes, r => r.BikeId, b => b.Id, (r, b) => new { r, b })
            .Join(fixture.Models, combined => combined.b.BikeModelId, m => m.Id, (combined, m) => new
            {
                ModelName = m.Name,
                Duration = combined.r.DurationHours,
                Profit = combined.r.DurationHours * m.HourlyPrice
            })
            .GroupBy(x => x.ModelName)
            .Select(g => new
            {
                ModelName = g.Key,
                TotalProfit = g.Sum(x => x.Profit),
                TotalDuration = g.Sum(x => x.Duration)
            })
            .ToList();

        var topByProfit = modelStats.OrderByDescending(x => x.TotalProfit).First();
        var topByDuration = modelStats.OrderByDescending(x => x.TotalDuration).First();

        Assert.Equal("Electro Bolt", topByProfit.ModelName);
        Assert.Equal(24300m, topByProfit.TotalProfit);

        Assert.Equal("Electro Bolt", topByDuration.ModelName);
        Assert.Equal(27, topByDuration.TotalDuration);
    }

    /// <summary>
    /// Вывести статистику времени аренды (мин, макс, среднее).
    /// </summary>
    [Fact]
    public void GetRentalStatistics_WhenCalculatingDurations_ReturnsCorrectAggregates()
    {
        var durations = fixture.Rentals.Select(r => (double)r.DurationHours).ToList();

        var min = durations.Min();
        var max = durations.Max();
        var avg = durations.Average();

        Assert.Equal(1, min);
        Assert.Equal(24, max);
        Assert.Equal(5.3, avg);
    }

    /// <summary>
    /// Вывести суммарное время аренды по типам.
    /// </summary>
    [Fact]
    public void GetTotalDuration_WhenGroupedByBikeType_ReturnsCorrectSumForMountainAndElectric()
    {
        var statsByType = fixture.Rentals
            .Join(fixture.Bikes, r => r.BikeId, b => b.Id, (r, b) => new { r, b })
            .Join(fixture.Models, combined => combined.b.BikeModelId, m => m.Id, (combined, m) => new
            {
                m.Type,
                combined.r.DurationHours
            })
            .GroupBy(x => x.Type)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.DurationHours));

        Assert.Equal(16, statsByType[BikeType.Mountain]);

        Assert.Equal(27, statsByType[BikeType.Electric]);
    }

    /// <summary>
    /// Вывести топ клиентов по количеству аренд.
    /// </summary>
    [Fact]
    public void GetTopRenters_WhenSortedByRentalCount_ReturnsIvanovFirst()
    {
        var topRenters = fixture.Rentals
            .GroupBy(r => r.RenterId)
            .Select(g => new
            {
                RenterId = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Join(fixture.Renters, stat => stat.RenterId, renter => renter.Id, (stat, renter) => new
            {
                renter.FullName,
                stat.Count
            })
            .ToList();

        Assert.Equal("Иванов Иван Иванович", topRenters[0].FullName);
        Assert.Equal(3, topRenters[0].Count);

        Assert.Equal("Петров Петр Петрович", topRenters[1].FullName);
        Assert.Equal(2, topRenters[1].Count);
    }
}