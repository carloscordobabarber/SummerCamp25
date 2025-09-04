using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using System.Threading.Tasks;
using DTOS;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChangePassController : ControllerBase
    {
        private readonly ContextDataBase _context;

        public ChangePassController(ContextDataBase context)
        {
            _context = context;
        }

        // POST: api/ChangePass
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassDto request)
        {
            if (request == null || request.UserId <= 0 || string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.OldPassword))
                return BadRequest("UserId, contraseña anterior y nueva contraseña son obligatorios.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            if (user.Password != request.OldPassword)
                return BadRequest("La contraseña anterior no es correcta.");

            user.Password = request.NewPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Contraseña actualizada correctamente.");
        }
    }
}
