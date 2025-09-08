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
    public class IncidencesController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<IncidencesController> _logger;

        public IncidencesController(ContextDataBase context, IMapper mapper, ILogger<IncidencesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Incidences
        [HttpGet]
        public async Task<ActionResult<object>> GetIncidences(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? issueType = null,
            [FromQuery] string? assignedCompany = null,
            [FromQuery] int? apartmentId = null,
            [FromQuery] int? rentalId = null,
            [FromQuery] int? tenantId = null,
            [FromQuery] string? statusId = null)
        {
            try
            {
                var query = _context.Incidences.AsNoTracking().AsQueryable();

                if (issueType.HasValue)
                    query = query.Where(i => i.IssueType == issueType.Value);
                if (!string.IsNullOrEmpty(assignedCompany))
                    query = query.Where(i => i.AssignedCompany.ToLower().Contains(assignedCompany.ToLower()));
                if (apartmentId.HasValue)
                    query = query.Where(i => i.ApartmentId == apartmentId.Value);
                if (rentalId.HasValue)
                    query = query.Where(i => i.RentalId == rentalId.Value);
                if (tenantId.HasValue)
                    query = query.Where(i => i.TenantId == tenantId.Value);
                if (!string.IsNullOrEmpty(statusId))
                    query = query.Where(i => i.StatusId == statusId);

                var totalCount = await query.CountAsync();
                var incidences = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var dto = _mapper.Map<List<IncidenceDto>>(incidences);
                _logger.LogInformation("Consulta de incidencias realizada correctamente. Total: {Count}", dto.Count);
                return Ok(new { totalCount, items = dto });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de incidencias");
                return StatusCode(500, $"Error al obtener la lista de incidencias: {ex.Message}");
            }
        }

        // GET: api/Incidences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidenceDto>> GetIncidence(int id)
        {
            try
            {
                var incidence = await _context.Incidences.FindAsync(id);
                if (incidence == null)
                {
                    _logger.LogWarning("Incidencia no encontrada para id {Id}", id);
                    return NotFound();
                }

                var dto = _mapper.Map<IncidenceDto>(incidence);
                _logger.LogInformation("Consulta de incidencia realizada correctamente para id {Id}", id);
                return Ok(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la incidencia con id {Id}", id);
                return StatusCode(500, $"Error al obtener la incidencia: {ex.Message}");
            }
        }

        // POST: api/Incidences
        [HttpPost]
        public async Task<IActionResult> PostIncidence([FromBody] IncidenceDto incidenceDto)
        {
            if (incidenceDto == null)
            {
                _logger.LogWarning("Intento de creación de incidencia fallido: DTO nulo");
                return BadRequest();
            }

            var incidence = _mapper.Map<Incidence>(incidenceDto);
            incidence.CreatedAt = DateTime.UtcNow;
            incidence.UpdatedAt = null;

            _context.Incidences.Add(incidence);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Incidencia creada correctamente con id {Id}", incidence.Id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la incidencia");
                return StatusCode(500, $"Error al guardar la incidencia: {ex.Message}");
            }

            var resultDto = _mapper.Map<IncidenceDto>(incidence);
            return CreatedAtAction(nameof(GetIncidence), new { id = incidence.Id }, resultDto);
        }

        // DELETE: api/Incidences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidence(int id)
        {
            try
            {
                var incidence = await _context.Incidences.FindAsync(id);
                if (incidence == null)
                {
                    _logger.LogWarning("Incidencia no encontrada para id {Id} al intentar eliminar", id);
                    return NotFound();
                }

                _context.Incidences.Remove(incidence);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Incidencia eliminada correctamente con id {Id}", id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la incidencia con id {Id}", id);
                return StatusCode(500, $"Error al eliminar la incidencia: {ex.Message}");
            }
        }
    }
}