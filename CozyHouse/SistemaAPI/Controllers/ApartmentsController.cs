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
    public class ApartmentsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ApartmentsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Apartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentWorkerDto>>> GetApartments()
        {
            var apartments = await _context.Apartments.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<ApartmentWorkerDto>>(apartments);
            return Ok(dto);
        }

        // GET: api/Apartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApartmentWorkerDto>> GetApartment(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
                return NotFound();

            var dto = _mapper.Map<ApartmentWorkerDto>(apartment);
            return Ok(dto);
        }

        // PUT: api/Apartments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(int id, [FromBody] ApartmentWorkerDto apartmentDto)
        {
            if (apartmentDto == null || id != apartmentDto.Id)
                return BadRequest();

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
                return NotFound();

            // Map fields except Id, CreatedAt
            apartment.Code = apartmentDto.Code;
            apartment.Door = apartmentDto.Door;
            apartment.Floor = apartmentDto.Floor;
            apartment.Price = apartmentDto.Price;
            apartment.Area = apartmentDto.Area;
            apartment.NumberOfRooms = apartmentDto.NumberOfRooms ?? 0;
            apartment.NumberOfBathrooms = apartmentDto.NumberOfBathrooms ?? 0;
            apartment.IsAvailable = apartmentDto.IsAvailable ?? false;
            apartment.BuildingId = apartmentDto.BuildingId;
            apartment.HasLift = apartmentDto.HasLift;
            apartment.HasGarage = apartmentDto.HasGarage;
            apartment.UpdatedAt = DateTime.UtcNow;

            _context.Entry(apartment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el apartamento: {ex.Message}");
            }

            return NoContent();
        }

        // PUT: api/Apartments/{id}/set-available
        [HttpPut("{id}/set-available")]
        public async Task<IActionResult> SetApartmentAvailable(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
                return NotFound();

            apartment.IsAvailable = true;
            apartment.UpdatedAt = DateTime.UtcNow;
            _context.Entry(apartment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el estado del apartamento: {ex.Message}");
            }

            return NoContent();
        }

        // PUT: api/Apartments/{id}/set-unavailable
        [HttpPut("{id}/set-unavailable")]
        public async Task<IActionResult> SetApartmentUnavailable(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
                return NotFound();

            apartment.IsAvailable = false;
            apartment.UpdatedAt = DateTime.UtcNow;
            _context.Entry(apartment).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el estado del apartamento: {ex.Message}");
            }

            return NoContent();
        }
    }
}