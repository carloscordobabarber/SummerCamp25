using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //crea una lista de apartamentos ficticios
            var apartments = new List<string>
            {
                "Apartamento 1: 2 habitaciones, 1 baño, cocina equipada",
                "Apartamento 2: 3 habitaciones, 2 baños, salón amplio",
                "Apartamento 3: 1 habitación, 1 baño, ideal para solteros",
                "Apartamento 4: 4 habitaciones, jardín privado, garaje"
            };
            //devuelve la lista de apartamentos como respuesta
            return Ok(apartments);
        }
        // Método para obtener un apartamento por ID
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Simula la búsqueda de un apartamento por ID
            var apartment = $"Apartamento {id}: 2 habitaciones, 1 baño, cocina equipada";

            // Si el apartamento no existe, devuelve NotFound
            if (id < 1 || id > 4)
            {
                return NotFound($"Apartamento con ID {id} no encontrado.");
            }

            // Devuelve el apartamento encontrado
            return Ok(apartment);

        }
    }
}
