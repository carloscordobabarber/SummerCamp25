using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using CozyData;
using Dominio;
using DTOS;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ELApartmentGetController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ELApartmentGetController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ELApartmentGet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentELDto>>> GetAll()
        {
            var apartments = await _context.Apartments.ToListAsync();
            var result = _mapper.Map<List<ApartmentELDto>>(apartments);
            return Ok(result);
        }

        // GET: api/ELApartmentGet/{apartmentCode}
        [HttpGet("{apartmentCode}")]
        public async Task<ActionResult<ApartmentELDto>> GetByCode(string apartmentCode)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.Code == apartmentCode);
            if (apartment == null)
                return NotFound();
            var result = _mapper.Map<ApartmentELDto>(apartment);
            return Ok(result);
        }
    }
}
