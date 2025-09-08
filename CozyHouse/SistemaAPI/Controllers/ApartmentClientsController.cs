using AutoMapper;
using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using Microsoft.Extensions.Logging;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentClientsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ApartmentClientsController> _logger;

        public ApartmentClientsController(ContextDataBase context, IMapper mapper, ILogger<ApartmentClientsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ApartmentClientDto>>> GetAvailableApartments([FromQuery] int pageSize = 10, [FromQuery] int page = 1)
        {
            if (pageSize <= 0) pageSize = 10;
            if (page <= 0) page = 1;

            try
            {
                var query = _context.Apartments.AsNoTracking().Where(a => a.IsAvailable);
                var totalCount = await query.CountAsync();
                var apartments = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var dto = _mapper.Map<List<ApartmentClientDto>>(apartments);
                var result = new PagedResult<ApartmentClientDto>
                {
                    Items = dto,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
                };
                _logger.LogInformation("Consulta de apartamentos disponibles realizada correctamente. Total: {Count}", totalCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los apartamentos disponibles");
                return StatusCode(500, $"Error al obtener los apartamentos disponibles: {ex.Message}");
            }
        }
    }
}