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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            var dto = _mapper.Map<List<UserDto>>(users);
            return Ok(dto);
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
            await _context.SaveChangesAsync();

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
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}