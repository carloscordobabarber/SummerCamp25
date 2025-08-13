using AutoMapper;
using Dominio;
using DTOS;

namespace SistemaAPI.Servicios
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Apartment, ApartmentDto>();
            CreateMap<Building, BuildingDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<DistrictStreet, DistrictStreetDto>();
            CreateMap<Incidence, IncidenceDto>();
            CreateMap<Log, LogDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Rental, RentalDto>();
            CreateMap<Status, StatusDto>();
            CreateMap<Street, StreetDto>();
            CreateMap<User, UserDto>();

            CreateMap<ApartmentDto, Apartment>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));



        }
    }
}