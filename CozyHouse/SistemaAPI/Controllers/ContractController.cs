// Archivo: ContractController.cs
using Microsoft.AspNetCore.Mvc;
using Dominio;
using System.Collections.Generic;
using System.Linq;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private static List<Contract> contracts = new List<Contract>
        {
            new Contract(1, "12345678A", "Juan Pérez", "ES1234567890123456789012", 500, new DateTime(2024, 1, 1), new DateTime(2024, 12, 31), "Calle Mayor 123"),
            new Contract(2, "87654321B", "Ana López", "ES0987654321098765432109", 600, new DateTime(2024, 3, 1), new DateTime(2025, 2, 28), "Avenida Real 45")
        };

        // GET: api/contract
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(contracts);
        }

        // GET: api/contract/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contract = contracts.FirstOrDefault(c => c.ContractId == id);

            if (contract == null)
            {
                return NotFound($"No se encontró ningún contrato con id {id}");
            }

            return Ok(contract);
        }

        // POST: api/contract
        [HttpPost]
        public IActionResult Post([FromBody] Contract contract)
        {
            contracts.Add(contract);
            return CreatedAtAction(nameof(GetById), new { id = contract.ContractId }, contract);
        }
    }
}
