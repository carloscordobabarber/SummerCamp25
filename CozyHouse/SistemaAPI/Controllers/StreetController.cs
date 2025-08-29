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
    public class StreetController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public StreetController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Street
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StreetDto>>> GetStreets()
        {
            var streets = await _context.Streets.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<StreetDto>>(streets);
            return Ok(dto);
        }

        // GET: api/Street/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StreetDto>> GetStreet(int id)
        {
            var street = await _context.Streets.FindAsync(id);
            if (street == null)
                return NotFound();

            var dto = _mapper.Map<StreetDto>(street);
            return Ok(dto);
        }

        // POST: api/Street
        [HttpPost]
        public async Task<IActionResult> PostStreet([FromBody] StreetDto streetDto)
        {
            if (streetDto == null)
                return BadRequest();

            var street = _mapper.Map<Street>(streetDto);
            street.CreatedAt = DateTime.UtcNow;
            street.UpdatedAt = null;

            _context.Streets.Add(street);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la calle: {ex.Message}");
            }

            var resultDto = _mapper.Map<StreetDto>(street);
            return CreatedAtAction(nameof(GetStreet), new { id = street.Id }, resultDto);
        }

        // PUT: api/Street/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStreet(int id, [FromBody] StreetDto streetDto)
        {
            if (streetDto == null || id != streetDto.Id)
                return BadRequest();

            var street = await _context.Streets.FindAsync(id);
            if (street == null)
                return NotFound();

            street.Code = streetDto.Code;
            street.Name = streetDto.Name;
            street.UpdatedAt = DateTime.UtcNow;

            _context.Entry(street).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la calle: {ex.Message}");
            }

            return NoContent();
        }

        // DELETE: api/Street/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStreet(int id)
        {
            var street = await _context.Streets.FindAsync(id);
            if (street == null)
                return NotFound();

            _context.Streets.Remove(street);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la calle: {ex.Message}");
            }

            return NoContent();
        }
    }
}