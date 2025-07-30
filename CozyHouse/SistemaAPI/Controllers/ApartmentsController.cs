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
            //new Apartment(1, "Calle Mayor 10, Madrid", 3, 18000, 2, 2, 90.0, true,"", new Building(1, "Calle Mayor 10, Madrid")),
            //new Apartment(2, "Av. Diagonal 245, Barcelona", 2, 14400, 3, 1, 70.0, true,"", new Building(2, "Av. Diagonal 245, Barcelona")),
            //new Apartment(3, "Gran Vía 100, Madrid", 4, 24000, 5, 2, 110.5, true,"", new Building(3, "Gran Vía 100, Madrid")),
            //new Apartment(4, "Paseo del Prado 20, Madrid", 1, 10800, 1, 1, 55.0, true,"", new Building(4, "Paseo del Prado 20, Madrid")),
            //new Apartment(5, "Rambla Nova 50, Tarragona", 3, 13200, 4, 2, 80.0, true,"", new Building(5, "Rambla Nova 50, Tarragona")),
            //new Apartment(6, "Calle de la Princesa 30, Madrid", 2, 15600, 3, 1, 75.0, true,"", new Building(6, "Calle de la Princesa 30, Madrid")),
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
