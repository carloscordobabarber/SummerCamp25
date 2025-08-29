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
    public class ApartmentClientsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ApartmentClientsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ApartmentClientDto>>> GetAvailableApartments([FromQuery] int pageSize = 10, [FromQuery] int page = 1)
        {
            if (pageSize <= 0) pageSize = 10;
            if (page <= 0) page = 1;

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
            return Ok(result);
        }
    }
}