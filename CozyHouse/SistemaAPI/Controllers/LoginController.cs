using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;

using SistemaAPI.Servicios;
using Microsoft.Extensions.Logging;


namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly JwtService _jwtService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ContextDataBase context, JwtService jwtService, ILogger<LoginController> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                _logger.LogWarning("Intento de login fallido: datos incompletos o nulos");
                return BadRequest("Email y contraseña son obligatorios.");
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
                _logger.LogWarning("Intento de login fallido: contraseña incorrecta para usuario con email {Email}", loginDto.Email);
                return Unauthorized("Contraseña incorrecta");
            }

            _logger.LogInformation("Login exitoso para usuario con email {Email}", loginDto.Email);
            var token = _jwtService.GenerateToken(user);
            return Ok(new { token, id = user.Id, role = user.Role });
        }
    }
}