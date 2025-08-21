using AutoMapper;
using Dominio;
using DTOS;

namespace SistemaAPI.Servicios
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Apartment, ApartmentDTO>();
            CreateMap<Apartment, ApartmentWorkerDto>();
            CreateMap<Apartment, ApartmentClientDto>();

            CreateMap<Building, BuildingDto>();
            CreateMap<Contact, ContactDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<DistrictStreet, DistrictStreetDto>();

            CreateMap<ImageApartment, ImagesDto>();

            CreateMap<Incidence, IncidenceDto>();

            CreateMap<Log, LogDto>();

            CreateMap<Payment, PaymentDto>();

            CreateMap<Rental, RentalDto>();

            CreateMap<Status, StatusDto>();

            CreateMap<Street, StreetDto>();

            CreateMap<User, UserDto>();
            CreateMap<User, UserWorkerDto>();

            CreateMap<ApartmentDTO, Apartment>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));



        }
    }
}