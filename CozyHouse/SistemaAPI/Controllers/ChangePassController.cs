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
                _logger.LogWarning("Intento de cambio de contraseña fallido: datos incompletos o nulos");
                return BadRequest("UserId, contraseña anterior y nueva contraseña son obligatorios.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user == null)
            {
                _logger.LogWarning("Intento de cambio de contraseña fallido: usuario id {UserId} no encontrado", request.UserId);
                return NotFound("Usuario no encontrado.");
            }

            if (user.Password != request.OldPassword)
            {
                _logger.LogWarning("Intento de cambio de contraseña fallido: contraseña anterior incorrecta para usuario id {UserId}", request.UserId);
                return BadRequest("La contraseña anterior no es correcta.");
            }

            user.Password = request.NewPassword;
            _context.Users.Update(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Contraseña actualizada correctamente para usuario id {UserId}", request.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la contraseña para usuario id {UserId}", request.UserId);
                return StatusCode(500, $"Error al actualizar la contraseña: {ex.Message}");
            }

            return Ok("Contraseña actualizada correctamente.");
        }
    }
}
