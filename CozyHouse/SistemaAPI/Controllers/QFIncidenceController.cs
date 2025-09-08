using Microsoft.AspNetCore.Mvc;
using Dominio;
using DTOS;
using AutoMapper;
using CozyData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QFIncidenceController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<QFIncidenceController> _logger;

        public QFIncidenceController(ContextDataBase context, IMapper mapper, ILogger<QFIncidenceController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // PATCH: api/QFIncidence/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchIncidence(int id, [FromBody] QFIncidencesDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("PATCH incidencia fallido: DTO nulo");
                return BadRequest();
            }

            var incidence = await _context.Incidences.FindAsync(id);
            if (incidence == null)
            {
                _logger.LogWarning("PATCH incidencia fallido: incidencia id {Id} no encontrada", id);
                return NotFound();
            }

            incidence.AssignedCompany = dto.AssignedCompany;
            incidence.StatusId = dto.StatusId;
            incidence.UpdatedAt = DateTime.UtcNow;

            _context.Entry(incidence).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Incidencia id {Id} actualizada correctamente", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la incidencia id {Id}", id);
                return StatusCode(500, $"Error al actualizar la incidencia: {ex.Message}");
            }

            return NoContent();
        }
    }
}
