using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using CozyData;
using Dominio;
using DTOS;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ELApartmentGetController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ELApartmentGetController> _logger;

        public ELApartmentGetController(ContextDataBase context, IMapper mapper, ILogger<ELApartmentGetController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/ELApartmentGet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentELDto>>> GetAll()
        {
            try
            {
                var apartments = await _context.Apartments.AsNoTracking().ToListAsync();
                var buildingIds = apartments.Select(a => a.BuildingId).Distinct().ToList();
                var buildings = await _context.Buildings.Where(b => buildingIds.Contains(b.Id)).ToListAsync();
                var result = apartments.Select(a => {
                    var dto = _mapper.Map<ApartmentELDto>(a);
                    var building = buildings.FirstOrDefault(b => b.Id == a.BuildingId);
                    dto.BuildingCode = building?.CodeBuilding ?? string.Empty;
                    return dto;
                }).ToList();
                _logger.LogInformation("Consulta de todos los apartamentos (EL) realizada correctamente. Total: {Count}", result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los apartamentos (EL)");
                return StatusCode(500, $"Error al obtener los apartamentos: {ex.Message}");
            }
        }

        // GET: api/ELApartmentGet/{apartmentCode}
        [HttpGet("{apartmentCode}")]
        public async Task<ActionResult<ApartmentELDto>> GetByCode(string apartmentCode)
        {
            try
            {
                var apartment = await _context.Apartments.AsNoTracking().FirstOrDefaultAsync(a => a.Code == apartmentCode);
                if (apartment == null)
                {
                    _logger.LogWarning("No se encontró el apartamento con código {ApartmentCode}", apartmentCode);
                    return NotFound($"No se encontró el apartamento con código '{apartmentCode}'.");
                }

                var building = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == apartment.BuildingId);
                var dto = _mapper.Map<ApartmentELDto>(apartment);
                dto.BuildingCode = building?.CodeBuilding ?? string.Empty;
                _logger.LogInformation("Consulta de apartamento (EL) realizada correctamente para código {ApartmentCode}", apartmentCode);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el apartamento (EL) con código {ApartmentCode}", apartmentCode);
                return StatusCode(500, $"Error al obtener el apartamento: {ex.Message}");
            }
        }
    }
}
