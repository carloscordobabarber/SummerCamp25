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
               
        }
    }
}