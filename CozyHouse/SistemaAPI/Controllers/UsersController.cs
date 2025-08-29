using AutoMapper;
using DTOS;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyData;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public UsersController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            return Ok(new { totalCount, items = dto });
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        // GET: api/Users/{id}/rentals
        [HttpGet("{id}/rentals")]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentalsByUserId(int id)
        {
            var rentals = await _context.Rentals
                .AsNoTracking()
                .Where(r => r.UserId == id)
                .ToListAsync();

            if (rentals == null || rentals.Count == 0)
                return NotFound();

            var dto = _mapper.Map<List<RentalDto>>(rentals);
            return Ok(dto);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserRegisterDto userDto)
        {
            if (userDto == null)
                return BadRequest();

            var user = _mapper.Map<User>(userDto);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = null;

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el usuario: {ex.Message}");
            }

            // Devuelve UserDto, que no incluye la contraseña
            var resultDto = _mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, resultDto);
        }

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, [FromBody] UserWorkerDto userDto)
        //{
        //    if (userDto == null || id != userDto.Id)
        //        return BadRequest();

        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //        return NotFound();

        //    // Map fields except Id, CreatedAt
        //    user.DocumentType = userDto.DocumentType;
        //    user.DocumentNumber = userDto.DocumentNumber;
        //    user.Name = userDto.Name;
        //    user.LastName = userDto.LastName;
        //    user.Email = userDto.Email;
        //    user.Password = userDto.Password;
        //    user.Role = userDto.Role;
        //    user.UpdatedAt = DateTime.UtcNow;

        //    _context.Entry(user).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el usuario: {ex.Message}");
            }

            return NoContent();
        }
    }
}