using AutoMapper;
using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using Microsoft.Extensions.Logging;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ApartmentsController> _logger;

        public ApartmentsController(ContextDataBase context, IMapper mapper, ILogger<ApartmentsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Apartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentWorkerDto>>> GetApartments()
        {
            try
            {
                var apartments = await _context.Apartments.AsNoTracking().ToListAsync();
                var dto = _mapper.Map<List<ApartmentWorkerDto>>(apartments);
                _logger.LogInformation("Consulta de apartamentos realizada correctamente. Total: {Count}", dto.Count);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de apartamentos");
                return StatusCode(500, $"Error al obtener la lista de apartamentos: {ex.Message}");
            }
        }

        // GET: api/Apartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApartmentWorkerDto>> GetApartment(int id)
        {
            try
            {
                var apartment = await _context.Apartments.FindAsync(id);
                if (apartment == null)
                {
                    _logger.LogWarning("Apartamento no encontrado para id {Id}", id);
                    return NotFound();
                }

                var dto = _mapper.Map<ApartmentWorkerDto>(apartment);
                _logger.LogInformation("Consulta de apartamento realizada correctamente para id {Id}", id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el apartamento con id {Id}", id);
                return StatusCode(500, $"Error al obtener el apartamento: {ex.Message}");
            }
        }

        // PUT: api/Apartments/{id}/set-available
        [HttpPut("{id}/set-available")]
        public async Task<IActionResult> SetApartmentAvailable(int id)
        {
            try
            {
                var apartment = await _context.Apartments.FindAsync(id);
                if (apartment == null)
                {
                    _logger.LogWarning("Apartamento no encontrado para id {Id} al intentar poner disponible", id);
                    return NotFound();
                }

                apartment.IsAvailable = true;
                apartment.UpdatedAt = DateTime.UtcNow;
                _context.Entry(apartment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Apartamento con id {Id} marcado como disponible", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del apartamento a disponible para id {Id}", id);
                return StatusCode(500, $"Error al actualizar el estado del apartamento: {ex.Message}");
            }
        }

        // PUT: api/Apartments/{id}/set-unavailable
        [HttpPut("{id}/set-unavailable")]
        public async Task<IActionResult> SetApartmentUnavailable(int id)
        {
            try
            {
                var apartment = await _context.Apartments.FindAsync(id);
                if (apartment == null)
                {
                    _logger.LogWarning("Apartamento no encontrado para id {Id} al intentar poner no disponible", id);
                    return NotFound();
                }

                apartment.IsAvailable = false;
                apartment.UpdatedAt = DateTime.UtcNow;
                _context.Entry(apartment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Apartamento con id {Id} marcado como no disponible", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del apartamento a no disponible para id {Id}", id);
                return StatusCode(500, $"Error al actualizar el estado del apartamento: {ex.Message}");
            }
        }
    }
}