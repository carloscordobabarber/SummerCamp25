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
    public class IncidencesController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public IncidencesController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var query = _context.Incidences.AsQueryable();

            if (issueType.HasValue)
                query = query.Where(i => i.IssueType == issueType.Value);
            if (!string.IsNullOrEmpty(assignedCompany))
                query = query.Where(i => i.AssignedCompany == assignedCompany);
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
            return Ok(new { totalCount, items = dto });
        }

        // GET: api/Incidences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidenceDto>> GetIncidence(int id)
        {
            var incidence = await _context.Incidences.FindAsync(id);
            if (incidence == null)
                return NotFound();

            var dto = _mapper.Map<IncidenceDto>(incidence);
            return Ok(dto);
        }

        // POST: api/Incidences
        [HttpPost]
        public async Task<IActionResult> PostIncidence([FromBody] IncidenceDto incidenceDto)
        {
            if (incidenceDto == null)
                return BadRequest();

            var incidence = _mapper.Map<Incidence>(incidenceDto);
            incidence.CreatedAt = DateTime.UtcNow;
            incidence.UpdatedAt = null;

            _context.Incidences.Add(incidence);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<IncidenceDto>(incidence);
            return CreatedAtAction(nameof(GetIncidence), new { id = incidence.Id }, resultDto);
        }

        // PUT: api/Incidences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidence(int id, [FromBody] IncidenceDto incidenceDto)
        {
            if (incidenceDto == null || id != incidenceDto.Id)
                return BadRequest();

            var incidence = await _context.Incidences.FindAsync(id);
            if (incidence == null)
                return NotFound();

            // Mapear campos excepto Id y CreatedAt
            incidence.Spokesperson = incidenceDto.Spokesperson;
            incidence.Description = incidenceDto.Description;
            incidence.IssueType = incidenceDto.IssueType;
            incidence.AssignedCompany = incidenceDto.AssignedCompany;
            incidence.ApartmentId = incidenceDto.ApartmentId;
            incidence.RentalId = incidenceDto.RentalId;
            incidence.TenantId = incidenceDto.TenantId;
            incidence.StatusId = incidenceDto.StatusId;
            incidence.UpdatedAt = DateTime.UtcNow;

            _context.Entry(incidence).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Incidences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidence(int id)
        {
            var incidence = await _context.Incidences.FindAsync(id);
            if (incidence == null)
                return NotFound();

            _context.Incidences.Remove(incidence);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}