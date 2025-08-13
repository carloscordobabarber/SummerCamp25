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
    public class ApartmentsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ApartmentsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostApartment([FromBody] ApartmentDto apartmentDto)
        {
            if (apartmentDto == null)
                return BadRequest();

            var apartment = _mapper.Map<Apartment>(apartmentDto);

            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ApartmentDto>(apartment);
            return CreatedAtAction(nameof(PostApartment), new { id = apartment.Id }, resultDto);
        }
    }
}