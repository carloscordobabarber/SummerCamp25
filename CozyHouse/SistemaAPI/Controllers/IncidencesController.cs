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
    public class IncidencesController : ControllerBase
    {
        private readonly ContextoAPI _context;

        public IncidencesController(ContextoAPI context)
        {
            _context = context;
        }

        // GET: api/Incidences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incidence>>> GetIncidences()
        {
            return await _context.Incidences.ToListAsync();
        }

        // GET: api/Incidences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Incidence>> GetIncidence(int id)
        {
            var incidence = await _context.Incidences.FindAsync(id);

            if (incidence == null)
            {
                return NotFound();
            }

            return incidence;
        }

        // PUT: api/Incidences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidence(int id, Incidence incidence)
        {
            if (id != incidence.IdIncidence)
            {
                return BadRequest();
            }

            _context.Entry(incidence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidenceExists(id))
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

        // POST: api/Incidences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Incidence>> PostIncidence(Incidence incidence)
        {
            _context.Incidences.Add(incidence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidence", new { id = incidence.IdIncidence }, incidence);
        }

        // DELETE: api/Incidences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidence(int id)
        {
            var incidence = await _context.Incidences.FindAsync(id);
            if (incidence == null)
            {
                return NotFound();
            }

            _context.Incidences.Remove(incidence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncidenceExists(int id)
        {
            return _context.Incidences.Any(e => e.IdIncidence == id);
        }
    }
}
