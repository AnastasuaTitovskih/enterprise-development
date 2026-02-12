using BikeRental.Domain.Model;

namespace BikeRental.Infrastructure.EfCore.Repository;

/// <summary>
/// Репозиторий для управления данными арендаторов.
/// </summary>
/// <param name="context">Контекст базы данных.</param>
public class RenterRepository(BikeRentalDbContext context) : BaseRepository<Renter>(context);