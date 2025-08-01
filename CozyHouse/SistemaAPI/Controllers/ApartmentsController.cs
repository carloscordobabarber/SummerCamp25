using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaAPI.Controllers
{
    [Route("api/apartments")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {

        // Crea una lista de apartamentos ficticios, cada uno con su Building
        List<Apartment> apartments = new List<Apartment>
        {
            new Apartment(1, "Calle Mayor 10, Madrid", 2, "D", 3, 18000, 2, 90.0, true,1),
            new Apartment(2, "Av. Diagonal 245, Barcelona", 3, "A", 2, 14400, 1, 70.0, true,1),
            new Apartment(3, "Gran Vía 100, Madrid", 2, "B", 4, 24000, 5, 110.5, true,2),
            new Apartment(4, "Paseo del Prado 20, Madrid", 1, "C", 1, 10800, 1, 55.0, true,2),
            new Apartment(5, "Rambla Nova 50, Tarragona", 2, "A", 3, 13200, 4, 80.0, true,3),
            new Apartment(6, "Calle de la Princesa 30, Madrid", 1, "D", 2, 15600, 3, 75.0, true,3)
        };

        [HttpGet]
        public IActionResult Get()
        {
            // Devuelve la lista de apartamentos como respuesta
            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Búsqueda de un apartamento por ID
            var apartment = apartments.FirstOrDefault(a => a.Id == id);


            // Si el apartamento no existe, devuelve NotFound
            if (apartment == null)
            {
                return NotFound($"Apartamento con ID {id} no encontrado.");
            }

            // Devuelve el apartamento encontrado
            return Ok(apartment);

        }
    }
}
