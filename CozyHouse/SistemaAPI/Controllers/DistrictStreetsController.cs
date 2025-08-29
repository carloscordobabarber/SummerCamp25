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
    public class DistrictStreetsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public DistrictStreetsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DistrictStreets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictStreetDto>>> GetDistrictStreets()
        {
            var districtStreets = await _context.DistrictStreets.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<DistrictStreetDto>>(districtStreets);
            return Ok(dto);
        }

        // GET: api/DistrictStreets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictStreetDto>> GetDistrictStreet(int id)
        {
            var districtStreet = await _context.DistrictStreets.FindAsync(id);
            if (districtStreet == null)
                return NotFound();
            var dto = _mapper.Map<DistrictStreetDto>(districtStreet);
            return Ok(dto);
        }

        // POST: api/DistrictStreets
        [HttpPost]
        public async Task<IActionResult> PostDistrictStreet([FromBody] DistrictStreetDto districtStreetDto)
        {
            if (districtStreetDto == null)
                return BadRequest();
            var districtStreet = _mapper.Map<DistrictStreet>(districtStreetDto);
            _context.DistrictStreets.Add(districtStreet);
            await _context.SaveChangesAsync();
            var resultDto = _mapper.Map<DistrictStreetDto>(districtStreet);
            return CreatedAtAction(nameof(GetDistrictStreet), new { id = districtStreet.Id }, resultDto);
        }

        // PUT: api/DistrictStreets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrictStreet(int id, [FromBody] DistrictStreetDto districtStreetDto)
        {
            if (districtStreetDto == null)
                return BadRequest();
            var districtStreet = await _context.DistrictStreets.FindAsync(id);
            if (districtStreet == null)
                return NotFound();
            districtStreet.DistrictId = districtStreetDto.DistrictId;
            districtStreet.StreetId = districtStreetDto.StreetId;
            _context.Entry(districtStreet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/DistrictStreets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrictStreet(int id)
        {
            var districtStreet = await _context.DistrictStreets.FindAsync(id);
            if (districtStreet == null)
                return NotFound();
            _context.DistrictStreets.Remove(districtStreet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
