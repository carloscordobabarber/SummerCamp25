// Archivo: ClientController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DTOS;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private static List<Client> clientes = new List<Client>
        {
            new Client(1, "12345678A", "Juan Pérez", "ES1234567890123456789012", "612345678", "garf@dfg.com", new DateTime(07,05,05)),
            new Client(2, "87654321B", "María García", "ES0987654321098765432109", "699998888", "garf@dfg.com", new DateTime(07, 05, 05)),
            new Client(3, "11223344C", "Carlos Ruiz", "ES2233445566778899001122", "655554444", "garf@dfg.com", new DateTime(07, 05, 05))
        };

        // GET: api/client
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(clientes);
        }

        // GET: api/client/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound($"No se encontró ningún cliente con id {id}");
            }

            return Ok(cliente);
        }

        // POST: api/client
        [HttpPost]
        public IActionResult Post([FromBody] Client client)
        {
            clientes.Add(client);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }


        //DELETE Cliente by id

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound($"No se encontró ningún cliente con id {id}");
            }

            clientes.Remove(cliente);
            return NoContent(); // 204: Operación exitosa sin contenido de respuesta
        }
    }
}
