using AutoMapper;
using BikeRental.Application.Contracts.BikeModel;
using BikeRental.Domain;
using BikeRental.Domain.Model;

namespace BikeRental.Application.Service;

/// <summary>
/// Сервис для управления моделями велосипедов.
/// </summary>
public class BikeModelService(IRepository<BikeModel, int> repository, IMapper mapper)
    : BaseApplicationService<BikeModel, BikeModelDto, BikeModelCreateUpdateDto>(repository, mapper);