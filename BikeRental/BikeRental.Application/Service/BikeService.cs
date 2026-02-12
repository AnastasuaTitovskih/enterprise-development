using AutoMapper;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Domain;
using BikeRental.Domain.Model;

namespace BikeRental.Application.Service;

/// <summary>
/// Сервис для управления велосипедами.
/// </summary>
public class BikeService(IRepository<Bike, int> repository, IMapper mapper)
    : BaseApplicationService<Bike, BikeDto, BikeCreateUpdateDto>(repository, mapper);