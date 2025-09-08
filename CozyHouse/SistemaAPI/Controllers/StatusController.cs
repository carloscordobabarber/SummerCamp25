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
    public class StatusController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<StatusController> _logger;

        public StatusController(ContextDataBase context, IMapper mapper, ILogger<StatusController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetStatuses()
        {
            try
            {
                var statuses = await _context.Statuses.AsNoTracking().ToListAsync();
                var dto = _mapper.Map<List<StatusDto>>(statuses);
                _logger.LogInformation("Consulta de todos los estados realizada correctamente. Total: {Count}", dto.Count);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de estados");
                return StatusCode(500, $"Error al obtener la lista de estados: {ex.Message}");
            }
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDto>> GetStatus(string id)
        {
            try
            {
                var status = await _context.Statuses.FindAsync(id);
                if (status == null)
                {
                    _logger.LogWarning("Estado no encontrado para id {Id}", id);
                    return NotFound();
                }

                var dto = _mapper.Map<StatusDto>(status);
                _logger.LogInformation("Consulta de estado realizada correctamente para id {Id}", id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el estado con id {Id}", id);
                return StatusCode(500, $"Error al obtener el estado: {ex.Message}");
            }
        }
    }
}