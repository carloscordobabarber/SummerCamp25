using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using SistemaAPI.Servicios;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly JwtService _jwtService;

        public LoginController(ContextDataBase context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                return BadRequest("Email y contraseña son obligatorios.");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null)
                return Unauthorized("Usuario no registrado.");

            if (!loginDto.Password.Equals(user.Password))
                return Unauthorized("Contraseña incorrecta");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}