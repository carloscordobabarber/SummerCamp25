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
        public async Task<ActionResult<IEnumerable<ApartmentClientDto>>> GetAvailableApartments()
        {
            var apartments = await _context.Apartments
                .Where(a => a.IsAvailable)
                .ToListAsync();

            var dto = _mapper.Map<List<ApartmentClientDto>>(apartments);
            return Ok(dto);
        }
    }
}