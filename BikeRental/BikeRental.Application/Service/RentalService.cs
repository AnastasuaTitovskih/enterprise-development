using AutoMapper;
using BikeRental.Application.Contracts.Rental;
using BikeRental.Domain;
using BikeRental.Domain.Model;

namespace BikeRental.Application.Service;

/// <summary>
/// Сервис для управления записями аренды.
/// </summary>
public class RentalService(IRepository<Rental, int> repository, IMapper mapper)
    : BaseApplicationService<Rental, RentalDto, RentalCreateUpdateDto>(repository, mapper) { }