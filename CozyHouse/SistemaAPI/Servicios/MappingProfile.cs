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
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<District, DistrictDto>();
            CreateMap<DistrictStreet, DistrictStreetDto>();

            CreateMap<ImageApartment, ImagesDto>();

            CreateMap<Incidence, IncidenceDto>().ReverseMap();

            CreateMap<Log, LogDto>();

            CreateMap<Payment, PaymentDto>();

            CreateMap<Rental, RentalDto>();

            CreateMap<Status, StatusDto>();

            CreateMap<Street, StreetDto>();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserWorkerDto>();
            CreateMap<UserRegisterDto, User>();

            CreateMap<ApartmentDTO, Apartment>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ApartmentPostDto, Apartment>();





        }
    }
}