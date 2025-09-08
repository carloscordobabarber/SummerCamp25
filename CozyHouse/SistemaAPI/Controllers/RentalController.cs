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
        public async Task<ActionResult<object>> GetRentals(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? userId = null,
            [FromQuery] int? apartmentId = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] string? statusId = null
        )
        {
            var rentalsQuery = _context.Rentals.AsNoTracking().AsQueryable();

            if (userId.HasValue)
                rentalsQuery = rentalsQuery.Where(r => r.UserId == userId.Value);

            if (apartmentId.HasValue)
                rentalsQuery = rentalsQuery.Where(r => r.ApartmentId == apartmentId.Value);

            if (startDate.HasValue)
                rentalsQuery = rentalsQuery.Where(r => r.StartDate >= startDate.Value);

            if (endDate.HasValue)
                rentalsQuery = rentalsQuery.Where(r => r.EndDate <= endDate.Value);

            if (!string.IsNullOrEmpty(statusId))
                rentalsQuery = rentalsQuery.Where(r => r.StatusId.ToLower().Contains(statusId.ToLower()));

            var totalCount = await rentalsQuery.CountAsync();
            var rentals = await rentalsQuery
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dto = _mapper.Map<List<RentalDto>>(rentals);
            return Ok(new
            {
                totalCount,
                items = dto
            });
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
    }
}