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

        // POST: api/Apartments
        [HttpPost]
        public async Task<IActionResult> PostApartment([FromBody] ApartmentWorkerDto apartmentDto)
        {
            if (apartmentDto == null)
                return BadRequest();

            var apartment = _mapper.Map<Apartment>(apartmentDto);
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

            var resultDto = _mapper.Map<ApartmentWorkerDto>(apartment);
            return CreatedAtAction(nameof(GetApartment), new { id = apartment.Id }, resultDto);
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

        // DELETE: api/Apartments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
                return NotFound();

            _context.Apartments.Remove(apartment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el apartamento: {ex.Message}");
            }

            return NoContent();
        }
    }
}