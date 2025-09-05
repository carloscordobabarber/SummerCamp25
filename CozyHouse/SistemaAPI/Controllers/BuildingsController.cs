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
    public class BuildingsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public BuildingsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingDto>>> GetBuildings()
        {
            var buildings = await _context.Buildings.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<BuildingDto>>(buildings);
            return Ok(dto);
        }

        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingDto>> GetBuilding(int id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
                return NotFound();
            var dto = _mapper.Map<BuildingDto>(building);
            return Ok(dto);
        }

        // PUT: api/Buildings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(int id, [FromBody] BuildingDto buildingDto)
        {
            if (buildingDto == null || id != buildingDto.Id)
                return BadRequest();
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
                return NotFound();
            building.CodeBuilding = buildingDto.CodeBuilding;
            building.CodeStreet = buildingDto.CodeStreet;
            building.Name = buildingDto.Name;
            building.Doorway = buildingDto.Doorway;
            building.UpdatedAt = DateTime.UtcNow;
            _context.Entry(building).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el edificio: {ex.Message}");
            }
            return NoContent();
        }
    }
}
