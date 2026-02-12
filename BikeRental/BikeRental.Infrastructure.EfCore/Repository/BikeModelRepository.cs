using BikeRental.Domain.Model;

namespace BikeRental.Infrastructure.EfCore.Repository;

/// <summary>
/// Репозиторий для управления моделями велосипедов.
/// </summary>
/// <param name="context">Контекст базы данных.</param>
public class BikeModelRepository(BikeRentalDbContext context) : BaseRepository<BikeModel>(context) { }