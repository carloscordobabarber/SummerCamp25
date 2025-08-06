// Archivo: IncidenceController.cs
using Microsoft.AspNetCore.Mvc;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using DTOS;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenceController : ControllerBase
    {
        private static List<Incidence> incidencias = new List<Incidence>
        {
            new Incidence(1, "Propietario 1", "Cliente 1", "Calle 1", "Fuga de agua", "Pendiente",DateTime.Now, DateTime.Now),
            new Incidence(2, "Propietario 2", "Cliente 2", "Calle 2", "Problemas eléctricos", "En Proceso", DateTime.Now, DateTime.Now)
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

            var log = new Log(
                DateTime.Now,
                "Creación de incidencia",
                "API Post Incidencia", // Puedes cambiar por el usuario real si lo tienes
                $"Incidencia creada con ID {nuevaIncidencia.IdIncidence}",
                "Incidence"
            );

            return CreatedAtAction(nameof(GetById), new { id = nuevaIncidencia.IdIncidence }, new { Incidence = nuevaIncidencia, Log = log });
        }

        //Metodo PUT
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] string newStatus)
        {
            var incidencia = incidencias.FirstOrDefault(i => i.IdIncidence == id);

            if (incidencia == null)
            {
                var log = new Log(
                    DateTime.Now,
                    "Actualización de estado",
                    "API",
                    $"Intento fallido de actualizar incidencia con ID {id}",
                    "Incidence"
                );
                return NotFound(new { Mensaje = $"No se encontró ninguna incidencia con id {id}", Log = log });
            }

            try
            {
                incidencia.UpdateStatus(newStatus);

                var log = new Log(
                    DateTime.Now,
                    "Actualización de estado",
                    "API",
                    $"Estado actualizado a '{newStatus}' para incidencia con ID {id}",
                    "Incidence"
                );

                return Ok(new { Incidence = incidencia, Log = log });
            }
            catch (ArgumentException ex)
            {
                var log = new Log(
                    DateTime.Now,
                    "Actualización de estado",
                    "API",
                    $"Error al actualizar incidencia con ID {id}: {ex.Message}",
                    "Incidence"
                );
                return BadRequest(new { Mensaje = ex.Message, Log = log });
            }
        }
    }
}
