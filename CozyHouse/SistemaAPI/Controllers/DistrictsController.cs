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
            var districts = await _context.Districts.ToListAsync();
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
            await _context.SaveChangesAsync();
            var resultDto = _mapper.Map<DistrictDto>(district);
            return CreatedAtAction(nameof(GetDistrict), new { id = district.Id }, resultDto);
        }

        // PUT: api/Districts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrict(int id, [FromBody] DistrictDto districtDto)
        {
            if (districtDto == null || id != districtDto.Id)
                return BadRequest();
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
                return NotFound();
            district.Name = districtDto.Name;
            district.Zipcode = districtDto.Zipcode;
            district.Country = districtDto.Country;
            district.City = districtDto.City;
            district.UpdatedAt = DateTime.UtcNow;
            _context.Entry(district).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Districts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
                return NotFound();
            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
