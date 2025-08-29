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
    public class RentalController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public RentalController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: api/Rental/CheckDate?apartmentId=1&date=2025-08-28T00:00:00.000Z
        [HttpGet("CheckDate")]
        public async Task<IActionResult> CheckDate([FromQuery] int apartmentId, [FromQuery] string date)
        {
            if (string.IsNullOrEmpty(date))
                return BadRequest(new { exists = false, message = "Fecha no proporcionada." });

            if (!DateTime.TryParse(date, out var startDate))
                return BadRequest(new { exists = false, message = "Formato de fecha inválido." });

            // Buscar si existe un alquiler para ese apartamento que solape con la fecha
            var exists = await _context.Rentals.AsNoTracking().AnyAsync(r =>
                r.ApartmentId == apartmentId &&
                r.StartDate <= startDate &&
                r.EndDate > startDate
            );

            if (exists)
            {
                return Ok(new { exists = true, message = "Ya existe un alquiler para este apartamento en la fecha seleccionada." });
            }
            else
            {
                return Ok(new { exists = false });
            }
        }

        // GET: api/Rental/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
                return NotFound();

            var dto = _mapper.Map<RentalDto>(rental);
            return Ok(dto);
        }

        // GET: api/Rental
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentals()
        {
            var rentals = await _context.Rentals.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<RentalDto>>(rentals);
            return Ok(dto);
        }

        // POST: api/Rental
        [HttpPost]
        public async Task<IActionResult> PostRental([FromBody] RentalDto rentalDto)
        {
            if (rentalDto == null)
                return BadRequest();

            var rental = _mapper.Map<Rental>(rentalDto);
            rental.StartDate = rentalDto.StartDate;
            rental.EndDate = rentalDto.EndDate;
            rental.CreatedAt = DateTime.UtcNow;
            rental.UpdatedAt = null;

            _context.Rentals.Add(rental);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el alquiler: {ex.Message}");
            }

            var resultDto = _mapper.Map<RentalDto>(rental);
            return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, resultDto);
        }

        // PUT: api/Rental/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, [FromBody] RentalDto rentalDto)
        {
            if (rentalDto == null || id != rentalDto.Id)
                return BadRequest();

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
                return NotFound();

            rental.UserId = rentalDto.UserId;
            rental.ApartmentId = rentalDto.ApartmentId;
            rental.StartDate = rentalDto.StartDate;
            rental.EndDate = rentalDto.EndDate;
            rental.StatusId = rentalDto.StatusId;
            // rental.UpdatedAt = DateTime.UtcNow;

            _context.Entry(rental).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el alquiler: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/Rental/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
                return NotFound();

            _context.Rentals.Remove(rental);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el alquiler: {ex.Message}");
            }

            return NoContent();
        }
    }
}