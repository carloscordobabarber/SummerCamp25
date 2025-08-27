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
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Apartments.AsQueryable();
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