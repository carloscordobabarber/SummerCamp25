using Microsoft.AspNetCore.Http;
// Archivo: ClientController.cs
using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        // GET api/client
        [HttpGet]
        public IActionResult Get()
        {
            var cliente = new Client(1, "12345678A", "Juan Pérez", "ES1234567890123456789012", "612345678");
            return Ok(cliente);
        }

        // POST api/client
        [HttpPost]
        public IActionResult Post([FromBody] Client client)
        {
            // Aquí podrías agregar lógica para guardar el cliente en una base de datos
            return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
        }
    }
}
