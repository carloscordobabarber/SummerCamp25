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
    public class ApartmentWorkersController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ApartmentWorkersController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetApartments(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null,
            [FromQuery] int? area = null,
            [FromQuery] int? numberOfRooms = null,
            [FromQuery] int? numberOfBathrooms = null,
            [FromQuery] bool? hasLift = null,
            [FromQuery] bool? hasGarage = null)
        {
            var query = _context.Apartments.AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(a => a.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(a => a.Price <= maxPrice.Value);
            if (area.HasValue)
                query = query.Where(a => a.Area <= area.Value);
            if (numberOfRooms.HasValue)
                query = query.Where(a => a.NumberOfRooms == numberOfRooms.Value);
            if (numberOfBathrooms.HasValue)
                query = query.Where(a => a.NumberOfBathrooms == numberOfBathrooms.Value);
            if (hasLift.HasValue)
                query = query.Where(a => a.HasLift == hasLift.Value);
            if (hasGarage.HasValue)
                query = query.Where(a => a.HasGarage == hasGarage.Value);

            var totalCount = await query.CountAsync();
            var apartments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var dto = _mapper.Map<List<ApartmentWorkerDto>>(apartments);
            return Ok(new { totalCount, items = dto });
        }
    }
}