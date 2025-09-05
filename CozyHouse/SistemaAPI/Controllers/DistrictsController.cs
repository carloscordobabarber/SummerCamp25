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
    public class DistrictsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public DistrictsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Districts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictDto>>> GetDistricts()
        {
            var districts = await _context.Districts.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<DistrictDto>>(districts);
            return Ok(dto);
        }

        // GET: api/Districts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictDto>> GetDistrict(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
                return NotFound();
            var dto = _mapper.Map<DistrictDto>(district);
            return Ok(dto);
        }

        // POST: api/Districts
        [HttpPost]
        public async Task<IActionResult> PostDistrict([FromBody] DistrictDto districtDto)
        {
            if (districtDto == null)
                return BadRequest();
            var district = _mapper.Map<District>(districtDto);
            district.CreatedAt = DateTime.UtcNow;
            district.UpdatedAt = null;
            _context.Districts.Add(district);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el distrito: {ex.Message}");
            }
            var resultDto = _mapper.Map<DistrictDto>(district);
            return CreatedAtAction(nameof(GetDistrict), new { id = district.Id }, resultDto);
        }
    }
}
