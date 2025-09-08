using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using System.Threading.Tasks;
using DTOS;
using Microsoft.Extensions.Logging;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChangePassController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly ILogger<ChangePassController> _logger;

        public ChangePassController(ContextDataBase context, ILogger<ChangePassController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST: api/ChangePass
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassDto request)
        {
            if (request == null || request.UserId <= 0 || string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.OldPassword))
            {
                _logger.LogWarning("Intento de cambio de contrase�a fallido: datos incompletos o nulos");
                return BadRequest("UserId, contrase�a anterior y nueva contrase�a son obligatorios.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user == null)
            {
                _logger.LogWarning("Intento de cambio de contrase�a fallido: usuario id {UserId} no encontrado", request.UserId);
                return NotFound("Usuario no encontrado.");
            }

            if (user.Password != request.OldPassword)
            {
                _logger.LogWarning("Intento de cambio de contrase�a fallido: contrase�a anterior incorrecta para usuario id {UserId}", request.UserId);
                return BadRequest("La contrase�a anterior no es correcta.");
            }

            user.Password = request.NewPassword;
            _context.Users.Update(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Contrase�a actualizada correctamente para usuario id {UserId}", request.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la contrase�a para usuario id {UserId}", request.UserId);
                return StatusCode(500, $"Error al actualizar la contrase�a: {ex.Message}");
            }

            return Ok("Contrase�a actualizada correctamente.");
        }
    }
}
