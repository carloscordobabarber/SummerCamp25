using AutoMapper;
using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity; // Añadido para hashing

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ContextDataBase context, IMapper mapper, ILogger<UsersController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<object>> GetUsers(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? documentNumber = null,
            [FromQuery] string? name = null,
            [FromQuery] string? lastName = null,
            [FromQuery] string? email = null,
            [FromQuery] string? phone = null,
            [FromQuery] string? role = null)
        {
            try
            {
                var query = _context.Users.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(documentNumber))
                    query = query.Where(u => u.DocumentNumber.ToLower().Contains(documentNumber.ToLower()));
                if (!string.IsNullOrEmpty(name))
                    query = query.Where(u => u.Name.ToLower().Contains(name.ToLower()));
                if (!string.IsNullOrEmpty(lastName))
                    query = query.Where(u => u.LastName.ToLower().Contains(lastName.ToLower()));
                if (!string.IsNullOrEmpty(email))
                    query = query.Where(u => u.Email.ToLower().Contains(email.ToLower()));
                if (!string.IsNullOrEmpty(phone))
                    query = query.Where(u => u.Phone.Contains(phone));
                if (!string.IsNullOrEmpty(role))
                    query = query.Where(u => u.Role.ToLower().Contains(role.ToLower()));

                var totalCount = await query.CountAsync();
                var users = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var dto = _mapper.Map<List<UserDto>>(users);
                _logger.LogInformation("Consulta de usuarios realizada correctamente. Total: {Count}", dto.Count);
                return Ok(new { totalCount, items = dto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de usuarios");
                return StatusCode(500, $"Error al obtener la lista de usuarios: {ex.Message}");
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("Usuario no encontrado para id {Id}", id);
                    return NotFound();
                }

                var dto = _mapper.Map<UserDto>(user);
                _logger.LogInformation("Consulta de usuario realizada correctamente para id {Id}", id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con id {Id}", id);
                return StatusCode(500, $"Error al obtener el usuario: {ex.Message}");
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserRegisterDto userDto)
        {
            if (userDto == null)
            {
                _logger.LogWarning("Intento de creación de usuario fallido: DTO nulo");
                return BadRequest();
            }

            var user = _mapper.Map<User>(userDto);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = null;
            user.StatusId = "A"; // Asignar 'A' por defecto (Activo)

            // Hash de la contraseña antes de guardar
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Usuario creado correctamente con id {Id}", user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el usuario");
                return StatusCode(500, $"Error al guardar el usuario: {ex.Message}");
            }

            // Devuelve UserDto, que no incluye la contraseña
            var resultDto = _mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, resultDto);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] UserWorkerDto userDto)
        {
            if (userDto == null || id != userDto.Id)
            {
                _logger.LogWarning("Intento de actualización de usuario fallido: DTO nulo o id no coincide");
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Usuario no encontrado para id {Id} al intentar actualizar", id);
                return NotFound();
            }

            // Map fields except Id, CreatedAt
            user.DocumentType = userDto.DocumentType;
            user.DocumentNumber = userDto.DocumentNumber;
            user.Name = userDto.Name;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.BirthDate = userDto.BirthDate;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Usuario actualizado correctamente con id {Id}", id);

            return NoContent();
        }

        [HttpPut("clientRole/{id}")]
        public async Task<IActionResult> PutUserRole(int id, [FromBody] UserRoleUpdateDto userRoleDto)
        {
            if (userRoleDto == null || string.IsNullOrEmpty(userRoleDto.Role))
            {
                _logger.LogWarning("Intento de actualización de rol de usuario fallido: DTO nulo o rol vacío");
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("Usuario no encontrado para id {Id} al intentar actualizar rol", id);
                return NotFound();
            }

            user.Role = userRoleDto.Role;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Rol de usuario actualizado correctamente para id {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol del usuario con id {Id}", id);
                return StatusCode(500, $"Error al actualizar el rol del usuario: {ex.Message}");
            }

            return NoContent();
        }
    }
}