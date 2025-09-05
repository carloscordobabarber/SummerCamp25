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
    public class LogsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public LogsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Logs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogDto>>> GetLogs()
        {
            var logs = await _context.Logs.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<LogDto>>(logs);
            return Ok(dto);
        }

        // GET: api/Logs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogDto>> GetLog(int id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null)
                return NotFound();

            var dto = _mapper.Map<LogDto>(log);
            return Ok(dto);
        }

        // POST: api/Logs
        [HttpPost]
        public async Task<IActionResult> PostLog([FromBody] LogDto logDto)
        {
            if (logDto == null)
                return BadRequest();

            var log = _mapper.Map<Log>(logDto);
            log.CreatedAt = DateTime.UtcNow;

            _context.Logs.Add(log);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el log: {ex.Message}");
            }

            var resultDto = _mapper.Map<LogDto>(log);
            return CreatedAtAction(nameof(GetLog), new { id = log.Id }, resultDto);
        }
    }
}