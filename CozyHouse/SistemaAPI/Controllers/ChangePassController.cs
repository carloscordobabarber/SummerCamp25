using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using System.Threading.Tasks;
using DTOS;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity; // Añadido para hashing
using Dominio; 

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
            if (request == null || string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.OldPassword))
            {
                _logger.LogWarning("Intento de cambio de contraseña fallido: datos incompletos o nulos");
                return BadRequest("Contraseña anterior y nueva contraseña son obligatorios.");
            }

            // Extraer el token del header Authorization
            var authHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                _logger.LogWarning("Token JWT no proporcionado o formato incorrecto");
                return Unauthorized("Token JWT no proporcionado o formato incorrecto.");
            }
            var token = authHeader.Substring("Bearer ".Length).Trim();

            int userId;
            try
            {
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub);
                if (subClaim == null || !int.TryParse(subClaim.Value, out userId))
                {
                    _logger.LogWarning("No se pudo extraer el Id de usuario del token");
                    return Unauthorized("No se pudo extraer el Id de usuario del token.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al leer el token JWT");
                return Unauthorized("Token JWT inválido.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Intento de cambio de contraseña fallido: usuario id {UserId} no encontrado", userId);
                return NotFound("Usuario no encontrado.");
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.OldPassword);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("Intento de cambio de contraseña fallido: contraseña anterior incorrecta para usuario id {UserId}", userId);
                return BadRequest("La contraseña anterior no es correcta.");
            }

            user.Password = passwordHasher.HashPassword(user, request.NewPassword);
            _context.Users.Update(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Contraseña actualizada correctamente para usuario id {UserId}", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la contraseña para usuario id {UserId}", userId);
                return StatusCode(500, $"Error al actualizar la contraseña: {ex.Message}");
            }

            return Ok("Contraseña actualizada correctamente.");
        }
    }
}

