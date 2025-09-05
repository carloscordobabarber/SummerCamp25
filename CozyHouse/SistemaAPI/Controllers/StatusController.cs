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
    }
}