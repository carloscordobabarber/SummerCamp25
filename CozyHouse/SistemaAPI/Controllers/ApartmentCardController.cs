
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
    public class ApartmentCardController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ApartmentCardController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ApartmentCard
        [HttpGet]
        public async Task<ActionResult<object>> GetApartments(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? districtId = null,
            [FromQuery] int? area = null,
            [FromQuery] bool? hasLift = null,
            [FromQuery] double? price = null,
            [FromQuery] bool? hasGarage = null,
            [FromQuery] int? numberOfRooms = null,
            [FromQuery] int? numberOfBathrooms = null
        )
        {
            var apartmentsQuery = _context.Apartments
                .Where(a => a.IsAvailable) // Solo apartamentos disponibles
                .AsQueryable();

            // Filtros directos sobre Apartment
            if (area.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.Area == area.Value);

            if (hasLift.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.HasLift == hasLift.Value);

            if (price.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.Price == price.Value);

            if (hasGarage.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.HasGarage == hasGarage.Value);

            if (numberOfRooms.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.NumberOfRooms == numberOfRooms.Value);

            if (numberOfBathrooms.HasValue)
                apartmentsQuery = apartmentsQuery.Where(a => a.NumberOfBathrooms == numberOfBathrooms.Value);

            var apartments = await apartmentsQuery.ToListAsync();
            var buildings = await _context.Buildings.ToListAsync();
            var districtStreets = await _context.DistrictStreets.ToListAsync();
            var districts = await _context.Districts.ToListAsync();
            var streets = await _context.Streets.ToListAsync();

            var apartmentDtos = new List<ApartmentCardsDto>();

            foreach (var apartment in apartments)
            {
                var dto = new ApartmentCardsDto
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
                    HasGarage = apartment.HasGarage
                };

                var building = buildings.FirstOrDefault(b => b.Id == apartment.BuildingId);
                string streetName = string.Empty;
                int dtoDistrictId = 0;
                string districtName = string.Empty;

                if (building != null)
                {
                    var street = streets.FirstOrDefault(s => s.Code == building.CodeStreet);
                    if (street != null)
                        streetName = street.Name;

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
                dto.StreetName = streetName;
                dto.DistrictId = dtoDistrictId;
                dto.DistrictName = districtName;

                dto.ImageUrls = await _context.imageApartments
                    .Where(img => img.ApartmentId == apartment.Id)
                    .Select(img => img.PhotoUrl)
                    .ToListAsync();

                apartmentDtos.Add(dto);
            }

            // Filtro por districtId (requiere haber resuelto la relaciï¿½n)
            if (districtId.HasValue)
                apartmentDtos = apartmentDtos.Where(a => a.DistrictId == districtId.Value).ToList();

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

        // GET: api/ApartmentCard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApartmentCardsDto>> GetApartment(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
                return NotFound();

            var building = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == apartment.BuildingId);
            var districtStreets = await _context.DistrictStreets.ToListAsync();
            var districts = await _context.Districts.ToListAsync();
            var streets = await _context.Streets.ToListAsync();

            var dto = new ApartmentCardsDto
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
                IsAvailable= apartment.IsAvailable
            };

            string streetName = string.Empty;
            int districtId = 0;
            string districtName = string.Empty;

            if (building != null)
            {
                var street = streets.FirstOrDefault(s => s.Code == building.CodeStreet);
                if (street != null)
                    streetName = street.Name;

                var streetId = street?.Id ?? 0;
                var districtStreet = districtStreets.FirstOrDefault(ds => ds.StreetId == streetId);
                if (districtStreet != null)
                {
                    var district = districts.FirstOrDefault(d => d.Id == districtStreet.DistrictId);
                    if (district != null)
                    {
                        districtId = district.Id;
                        districtName = district.Name;
                    }
                }
            }
            dto.StreetName = streetName;
            dto.DistrictId = districtId;
            dto.DistrictName = districtName;

            dto.ImageUrls = await _context.imageApartments
                .Where(img => img.ApartmentId == apartment.Id)
                .Select(img => img.PhotoUrl)
                .ToListAsync();

            return Ok(dto);
        }
    }
}
