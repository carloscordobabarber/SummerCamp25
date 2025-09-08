using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using Microsoft.Extensions.Logging;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ContextDataBase context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                _logger.LogWarning("Intento de login fallido: datos incompletos o nulos");
                return BadRequest("Email y contrase�a son obligatorios.");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null)
            {
                _logger.LogWarning("Intento de login fallido: usuario no registrado con email {Email}", loginDto.Email);
                return Unauthorized("Usuario no registrado.");
            }

            if (!loginDto.Password.Equals(user.Password))
            {
                _logger.LogWarning("Intento de login fallido: contrase�a incorrecta para usuario con email {Email}", loginDto.Email);
                return Unauthorized("Contrase�a incorrecta");
            }

            _logger.LogInformation("Login exitoso para usuario con email {Email}", loginDto.Email);
            return Ok(new { id = user.Id, role = user.Role });
        }
    }
}