using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using Dominio;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Apartments1Controller : ControllerBase
    {
        private readonly ContextoAPI _context;

        public Apartments1Controller(ContextoAPI context)
        {
            _context = context;
        }

        // GET: api/Apartments1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartments()
        {
            return await _context.Apartments.ToListAsync();
        }

        // GET: api/Apartments1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Apartment>> GetApartment(Guid id)
        {
            var apartment = await _context.Apartments.FindAsync(id);

            if (apartment == null)
            {
                return NotFound();
            }

            return apartment;
        }

        // PUT: api/Apartments1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(Guid id, Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return BadRequest();
            }

            _context.Entry(apartment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Apartments1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Apartment>> PostApartment(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApartment", new { id = apartment.Id }, apartment);
        }

        // DELETE: api/Apartments1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(Guid id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApartmentExists(Guid id)
        {
            return _context.Apartments.Any(e => e.Id == id);
        }
    }
}
