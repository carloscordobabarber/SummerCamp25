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
    public class StatusController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public StatusController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetStatuses()
        {
            var statuses = await _context.Statuses.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<StatusDto>>(statuses);
            return Ok(dto);
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDto>> GetStatus(string id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
                return NotFound();

            var dto = _mapper.Map<StatusDto>(status);
            return Ok(dto);
        }

        // POST: api/Status
        [HttpPost]
        public async Task<IActionResult> PostStatus([FromBody] StatusDto statusDto)
        {
            if (statusDto == null)
                return BadRequest();

            var status = _mapper.Map<Status>(statusDto);
            status.CreatedAt = DateTime.UtcNow;
            status.UpdatedAt = null;

            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<StatusDto>(status);
            return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, resultDto);
        }

        // PUT: api/Status/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(string id, [FromBody] StatusDto statusDto)
        {
            if (statusDto == null || id != statusDto.Id)
                return BadRequest();

            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
                return NotFound();

            status.Name = statusDto.Name;
            status.UpdatedAt = DateTime.UtcNow;

            _context.Entry(status).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(string id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
                return NotFound();

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}