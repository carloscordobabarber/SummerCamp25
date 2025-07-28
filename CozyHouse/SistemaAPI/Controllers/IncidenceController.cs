// Archivo: IncidenceController.cs
using Microsoft.AspNetCore.Mvc;
using Dominio;
using System.Collections.Generic;
using System.Linq;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenceController : ControllerBase
    {
        private static List<Incidence> incidencias = new List<Incidence>
        {
            new Incidence(1, "Propietario 1", "Cliente 1", "Calle 1", "Fuga de agua", "Pendiente"),
            new Incidence(2, "Propietario 2", "Cliente 2", "Calle 2", "Problemas eléctricos", "En Proceso")
        };

        // GET: api/incidence
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(incidencias);
        }

        // GET: api/incidence/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var incidencia = incidencias.FirstOrDefault(i => i.IdIncidence == id);

            if (incidencia == null)
            {
                return NotFound($"No se encontró ninguna incidencia con id {id}");
            }

            return Ok(incidencia);
        }

        // POST: api/incidence
        [HttpPost]
        public IActionResult Post([FromBody] Incidence nuevaIncidencia)
        {
            incidencias.Add(nuevaIncidencia);
            return CreatedAtAction(nameof(GetById), new { id = nuevaIncidencia.IdIncidence }, nuevaIncidencia);
        }

        //Metodo PUT

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] string newStatus)
        {
            var incidencia = incidencias.FirstOrDefault(i => i.IdIncidence == id);

            if (incidencia == null)
            {
                return NotFound($"No se encontró ninguna incidencia con id {id}");
            }

            try
            {
                incidencia.UpdateStatus(newStatus);
                return Ok(incidencia);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
