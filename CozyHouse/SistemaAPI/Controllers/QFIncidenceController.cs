using Microsoft.AspNetCore.Mvc;
using Dominio;
using DTOS;
using AutoMapper;
using CozyData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QFIncidenceController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public QFIncidenceController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // PATCH: api/QFIncidence/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchIncidence(int id, [FromBody] QFIncidencesDto dto)
        {
            if (dto == null)
                return BadRequest();

            var incidence = await _context.Incidences.FindAsync(id);
            if (incidence == null)
                return NotFound();

            incidence.AssignedCompany = dto.AssignedCompany;
            incidence.StatusId = dto.StatusId;
            incidence.UpdatedAt = DateTime.UtcNow;

            _context.Entry(incidence).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la incidencia: {ex.Message}");
            }

            return NoContent();
        }
    }
}
