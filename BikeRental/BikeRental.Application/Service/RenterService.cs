using AutoMapper;
using BikeRental.Application.Contracts.Renter;
using BikeRental.Domain;
using BikeRental.Domain.Model;

namespace BikeRental.Application.Service;

/// <summary>
/// Сервис для управления арендаторами.
/// </summary>
public class RenterService(IRepository<Renter, int> repository, IMapper mapper)
    : BaseApplicationService<Renter, RenterDto, RenterCreateUpdateDto>(repository, mapper);