using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Analytics;
using BikeRental.Application.Contracts.Renter;
using BikeRental.Domain;
using BikeRental.Domain.Enum;
using BikeRental.Domain.Model;

namespace BikeRental.Application.Service;

/// <summary>
/// Сервис аналитики.
/// Реализует выборки и отчеты по данным проката.
/// </summary>
/// <param name="rentalRepository">Репозиторий аренды.</param>
/// <param name="bikeRepository">Репозиторий велосипедов.</param>
/// <param name="mapper">Маппер объектов.</param>
public class AnalyticsService(
    IRepository<Rental, int> rentalRepository,
    IRepository<Bike, int> bikeRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<IList<SportBikeDto>> GetSportBikes()
    {
        var allBikes = await bikeRepository.GetAll();
        var sportTypes = new[] { BikeType.Mountain, BikeType.Road };

        return [.. allBikes
            .Where(b => b.Model != null && sportTypes.Contains(b.Model.Type))
            .Select(b => new SportBikeDto(
                b.Id,
                b.SerialNumber,
                b.Model!.Name,
                b.Model.Type,
                b.Model.HourlyPrice
            ))];
    }

    /// <inheritdoc/>
    public async Task<IList<TopModelProfitDto>> GetTopModelsByProfit(int count = 5)
    {
        var rentals = await rentalRepository.GetAll();

        return [.. rentals
            .Where(r => r.Bike?.Model != null)
            .GroupBy(r => r.Bike!.Model!.Name)
            .Select(g => new TopModelProfitDto(
                g.Key,
                g.Sum(r => r.DurationHours * r.Bike!.Model!.HourlyPrice)
            ))
            .OrderByDescending(x => x.TotalProfit)
            .Take(count)];
    }

    /// <inheritdoc/>
    public async Task<IList<TopModelDurationDto>> GetTopModelsByDuration(int count = 5)
    {
        var rentals = await rentalRepository.GetAll();

        return [.. rentals
            .Where(r => r.Bike?.Model != null)
            .GroupBy(r => r.Bike!.Model!.Name)
            .Select(g => new TopModelDurationDto(
                g.Key,
                g.Sum(r => r.DurationHours)
            ))
            .OrderByDescending(x => x.TotalDurationHours)
            .Take(count)];
    }

    /// <inheritdoc/>
    public async Task<RentalDurationStatsDto?> GetRentalDurationStats()
    {
        var rentals = await rentalRepository.GetAll();

        if (rentals.Count == 0)
        {
            return null;
        }

        var durations = rentals.Select(r => r.DurationHours).ToList();

        return new RentalDurationStatsDto(
            durations.Min(),
            durations.Max(),
            durations.Average()
        );
    }

    /// <inheritdoc/>
    public async Task<IList<BikeTypeDurationStatDto>> GetDurationByBikeType()
    {
        var rentals = await rentalRepository.GetAll();

        return [.. rentals
            .Where(r => r.Bike?.Model != null)
            .GroupBy(r => r.Bike!.Model!.Type)
            .Select(g => new BikeTypeDurationStatDto(
                g.Key,
                g.Sum(r => r.DurationHours)
            ))];
    }

    /// <inheritdoc/>
    public async Task<IList<RenterActivityDto>> GetTopRenters(int count = 5)
    {
        var rentals = await rentalRepository.GetAll();

        var topRentersData = rentals
            .Where(r => r.Renter != null)
            .GroupBy(r => r.RenterId)
            .Select(g => new
            {
                Renter = g.First().Renter!,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(count)
            .ToList();

        return [.. topRentersData
            .Select(x => new RenterActivityDto(
                mapper.Map<RenterDto>(x.Renter),
                x.Count
            ))];
    }
}