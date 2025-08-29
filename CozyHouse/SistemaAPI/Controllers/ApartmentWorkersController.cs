using AutoMapper;
using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentWorkersController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ApartmentWorkersController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetApartments(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? districtId = null,
            [FromQuery] int? area = null,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null,
            [FromQuery] int? numberOfRooms = null,
            [FromQuery] int? numberOfBathrooms = null,
            [FromQuery] string? door = null,
            [FromQuery] string? code = null,
            [FromQuery] string? streetName = null
        )
        {
            var apartmentsQuery = _context.Apartments.AsNoTracking().AsQueryable();

            // Filtros directos sobre Apartment
            if (area.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.Area <= area.Value);

            if (minPrice.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.Price <= maxPrice.Value);

            if (numberOfRooms.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.NumberOfRooms == numberOfRooms.Value);

            if (numberOfBathrooms.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.NumberOfBathrooms == numberOfBathrooms.Value);

            if (!string.IsNullOrEmpty(door))
                apartmentsQuery = apartmentsQuery.Where(a => a.Door.ToLower().Contains(door.ToLower()));

            if (!string.IsNullOrEmpty(code))
                apartmentsQuery = apartmentsQuery.Where(a => a.Code.ToLower().Contains(code.ToLower()));

            var apartments = await apartmentsQuery.ToListAsync();
            var buildings = await _context.Buildings.AsNoTracking().ToListAsync();
            var districtStreets = await _context.DistrictStreets.AsNoTracking().ToListAsync();
            var districts = await _context.Districts.AsNoTracking().ToListAsync();
            var streets = await _context.Streets.AsNoTracking().ToListAsync();

            var apartmentDtos = new List<ApartmentWorkerDto>();

            foreach (var apartment in apartments)
            {
                var dto = new ApartmentWorkerDto
                {
                    Id = apartment.Id,
                    Code = apartment.Code,
                    Door = apartment.Door,
                    Floor = apartment.Floor,
                    Price = apartment.Price,
                    Area = apartment.Area,
                    NumberOfRooms = apartment.NumberOfRooms,
                    NumberOfBathrooms = apartment.NumberOfBathrooms,
                    BuildingId = apartment.BuildingId,
                    HasLift = apartment.HasLift,
                    HasGarage = apartment.HasGarage,
                    IsAvailable = apartment.IsAvailable,
                    CreatedAt = apartment.CreatedAt,
                    UpdatedAt = apartment.UpdatedAt
                };

                var building = buildings.FirstOrDefault(b => b.Id == apartment.BuildingId);
                string streetNameValue = string.Empty;
                int dtoDistrictId = 0;
                string districtName = string.Empty;

                if (building != null)
                {
                    var street = streets.FirstOrDefault(s => s.Code == building.CodeStreet);
                    if (street != null)
                        streetNameValue = street.Name;

                    var streetId = street?.Id ?? 0;
                    var districtStreet = districtStreets.FirstOrDefault(ds => ds.StreetId == streetId);
                    if (districtStreet != null)
                    {
                        var district = districts.FirstOrDefault(d => d.Id == districtStreet.DistrictId);
                        if (district != null)
                        {
                            dtoDistrictId = district.Id;
                            districtName = district.Name;
                        }
                    }
                }
                dto.StreetName = streetNameValue;
                dto.DistrictId = dtoDistrictId;
                dto.DistrictName = districtName;

                apartmentDtos.Add(dto);
            }

            // Filtro por districtId (requiere haber resuelto la relación)
            if (districtId.HasValue)
                apartmentDtos = apartmentDtos.Where(a => a.DistrictId == districtId.Value).ToList();

            // Filtro por streetName (case-insensitive, contiene)
            if (!string.IsNullOrEmpty(streetName))
                apartmentDtos = apartmentDtos.Where(a => !string.IsNullOrEmpty(a.StreetName) && a.StreetName.ToLower().Contains(streetName.ToLower())).ToList();

            var totalCount = apartmentDtos.Count;
            var pagedResult = apartmentDtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                totalCount,
                items = pagedResult
            });
        }
    }
}