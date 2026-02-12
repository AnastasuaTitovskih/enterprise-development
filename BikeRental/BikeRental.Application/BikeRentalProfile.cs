using AutoMapper;
using BikeRental.Domain.Model;
using BikeRental.Application.Contracts.Bike;
using BikeRental.Application.Contracts.BikeModel;
using BikeRental.Application.Contracts.Rental;
using BikeRental.Application.Contracts.Renter;

namespace BikeRental.Application;

/// <summary>
/// Профиль маппинга AutoMapper.
/// Определяет правила преобразования между доменными сущностями и DTO.
/// </summary>
public class BikeRentalProfile : Profile
{
    public BikeRentalProfile()
    {
        CreateMap<BikeModel, BikeModelDto>();
        CreateMap<BikeModelCreateUpdateDto, BikeModel>();

        CreateMap<Bike, BikeDto>();
        CreateMap<BikeCreateUpdateDto, Bike>();

        CreateMap<Renter, RenterDto>();
        CreateMap<RenterCreateUpdateDto, Renter>();

        CreateMap<Rental, RentalDto>();
        CreateMap<RentalCreateUpdateDto, Rental>();
    }
}