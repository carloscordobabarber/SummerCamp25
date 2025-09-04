using AutoMapper;
using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using CozyData;
using Microsoft.EntityFrameworkCore;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QPPostController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public QPPostController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: api/QPPost/district
        [HttpPost("district")]
        public async Task<IActionResult> PostDistrict([FromBody] DistrictDto districtDto)
        {
            if (districtDto == null)
                return BadRequest();
            var district = _mapper.Map<District>(districtDto);
            district.CreatedAt = DateTime.UtcNow;
            district.UpdatedAt = null;
            _context.Districts.Add(district);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el distrito: {ex.Message}");
            }
            var resultDto = _mapper.Map<DistrictDto>(district);
            return CreatedAtAction(nameof(PostDistrict), new { id = district.Id }, resultDto);
        }

        // POST: api/QPPost/street
        [HttpPost("street")]
        public async Task<IActionResult> PostStreet([FromBody] StreetQPDto streetQPDto)
        {
            if (streetQPDto == null)
                return BadRequest("El body no puede ser nulo.");

            // Usar la propiedad DistrictZipCode del DTO
            var zipcode = streetQPDto.DistrictZipCode;
            if (string.IsNullOrWhiteSpace(zipcode))
                return BadRequest("El DistrictZipCode es obligatorio.");

            // Buscar el distrito por zipcode
            var district = await _context.Districts.FirstOrDefaultAsync(d => d.Zipcode == zipcode);
            if (district == null)
                return NotFound($"No existe un distrito con el zipcode '{zipcode}'.");
            var districtId = district.Id;

            // Mapear a StreetDto y luego a Street
            var streetDto = _mapper.Map<StreetDto>(streetQPDto);
            var street = _mapper.Map<Street>(streetDto);
            street.CreatedAt = DateTime.UtcNow;
            street.UpdatedAt = null;

            // Iniciar transacción
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Guardar la calle
                _context.Streets.Add(street);
                await _context.SaveChangesAsync();
                var streetId = street.Id;

                // Comprobar unicidad de la relación
                bool exists = await _context.DistrictStreets.AnyAsync(ds => ds.DistrictId == districtId && ds.StreetId == streetId);
                if (exists)
                {
                    await transaction.RollbackAsync();
                    return Conflict("La relación distrito-calle ya existe.");
                }

                // Guardar la relación
                var districtStreet = new DistrictStreet { DistrictId = districtId, StreetId = streetId };
                _context.DistrictStreets.Add(districtStreet);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return CreatedAtAction(nameof(PostStreet), new { id = streetId }, new { StreetId = streetId, DistrictId = districtId });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error al guardar la calle y su relación: {ex.Message}");
            }
        }

        // POST: api/QPPost/building
        [HttpPost("building")]
        public async Task<IActionResult> PostBuilding([FromBody] BuildingDto buildingDto)
        {
            if (buildingDto == null)
                return BadRequest("El body no puede ser nulo.");

            // Comprobar que existe el CodeStreet en la tabla Street
            var streetExists = await _context.Streets.AnyAsync(s => s.Code == buildingDto.CodeStreet);
            if (!streetExists)
                return NotFound($"No existe una calle con el código '{buildingDto.CodeStreet}'.");

            var building = _mapper.Map<Building>(buildingDto);
            building.CreatedAt = DateTime.UtcNow;
            building.UpdatedAt = null;
            building.StatusId = "A"; // Asignar StatusId a "A"

            _context.Buildings.Add(building);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el edificio: {ex.Message}");
            }
            var resultDto = _mapper.Map<BuildingDto>(building);
            return CreatedAtAction(nameof(PostBuilding), new { id = building.Id }, resultDto);
        }

        // POST: api/QPPost/apartment
        [HttpPost("apartment")]
        public async Task<IActionResult> PostApartment([FromBody] ApartmentQPDto apartmentQPDto)
        {
            if (apartmentQPDto == null)
                return BadRequest("El body no puede ser nulo.");

            // Comprobar que existe el BuildingCode en la tabla Building
            var building = await _context.Buildings.FirstOrDefaultAsync(b => b.CodeBuilding == apartmentQPDto.BuildingCode);
            if (building == null)
                return NotFound($"No existe un edificio con el código '{apartmentQPDto.BuildingCode}'.");

            // Mapear a ApartmentPostDto y luego a Apartment
            var apartmentPostDto = _mapper.Map<ApartmentPostDto>(apartmentQPDto);
            apartmentPostDto.BuildingId = building.Id;
            apartmentPostDto.IsAvailable = true;
            apartmentPostDto.StatusId = "E";

            var apartment = _mapper.Map<Apartment>(apartmentPostDto);
            apartment.CreatedAt = DateTime.UtcNow;
            apartment.UpdatedAt = null;

            _context.Apartments.Add(apartment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el apartamento: {ex.Message}");
            }
            return CreatedAtAction(nameof(PostApartment), new { id = apartment.Id }, apartment);
        }
    }
}
