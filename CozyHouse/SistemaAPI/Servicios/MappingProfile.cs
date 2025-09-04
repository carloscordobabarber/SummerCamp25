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
            CreateMap<Apartment, ApartmentELDto>()
                .ForMember(dest => dest.ApartmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ApartmentCode, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.ApartmentDoor, opt => opt.MapFrom(src => src.Door))
                .ForMember(dest => dest.ApartmentFloor, opt => opt.MapFrom(src => src.Floor.ToString()))
                .ForMember(dest => dest.ApartmentPrice, opt => opt.MapFrom(src => Math.Round((decimal)src.Price, 2)))
                .ForMember(dest => dest.NumberOfRooms, opt => opt.MapFrom(src => src.NumberOfRooms))
                .ForMember(dest => dest.NumberOfBathrooms, opt => opt.MapFrom(src => src.NumberOfBathrooms))
                .ForMember(dest => dest.BuildingId, opt => opt.MapFrom(src => src.BuildingId))
                .ForMember(dest => dest.HasLift, opt => opt.MapFrom(src => src.HasLift))
                .ForMember(dest => dest.HasGarage, opt => opt.MapFrom(src => src.HasGarage))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<Building, BuildingDto>();
            CreateMap<BuildingDto, Building>();

            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<District, DistrictDto>();
            CreateMap<DistrictDto, District>();
            CreateMap<DistrictStreet, DistrictStreetDto>();

            CreateMap<ImageApartment, ImagesDto>();

            CreateMap<Incidence, IncidenceDto>().ReverseMap();

            CreateMap<Log, LogDto>();

            CreateMap<Payment, PaymentDto>().ReverseMap();

            CreateMap<Rental, RentalDto>();
            CreateMap<RentalDto, Rental>();

            CreateMap<Status, StatusDto>();

            CreateMap<Street, StreetDto>();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserWorkerDto>();
            CreateMap<UserRegisterDto, User>();

            CreateMap<ApartmentDTO, Apartment>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ApartmentPostDto, Apartment>();

            // NUEVO: StreetQPDto -> StreetDto y Street
            CreateMap<StreetQPDto, StreetDto>();
            CreateMap<StreetQPDto, Street>();

            // NUEVO: ApartmentQPDto -> ApartmentPostDto
            CreateMap<ApartmentQPDto, ApartmentPostDto>()
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => "E"));

            CreateMap<StreetDto, Street>();
            CreateMap<Street, StreetDto>();
        }
    }
}