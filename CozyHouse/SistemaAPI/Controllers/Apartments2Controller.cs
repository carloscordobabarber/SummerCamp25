using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using Dominio;
using AutoMapper;
using DTOS;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Apartments2Controller : ControllerBase
    {
        private readonly ContextoAPI _context;
        private readonly IMapper _mapper;

        public Apartments2Controller(ContextoAPI context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Apartments2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentDTO>>> GetApartments()
        {
            var apartments = await _context.Apartments
                .Where(apartment => apartment.IsAvailable == true)
                .OrderBy(apartment => apartment.Area)
                .ToListAsync();

            var apartmentsDto = _mapper.Map<List<ApartmentDTO>>(apartments);

            return Ok(apartmentsDto);
        }

        // ... resto del controlador ...
    }

}
