using AutoMapper;
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
    public class ContactsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ContextDataBase context, IMapper mapper, ILogger<ContactsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<object>> GetContacts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? contactReason = null,
            [FromQuery] string? email = null)
        {
            try
            {
                var contactsQuery = _context.Set<Contact>().AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(contactReason))
                    contactsQuery = contactsQuery.Where(c => c.ContactReason == contactReason);

                if (!string.IsNullOrEmpty(email))
                    contactsQuery = contactsQuery.Where(c => c.Email.ToLower().Contains(email.ToLower()));

                var totalCount = await contactsQuery.CountAsync();

                var contacts = await contactsQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var dto = _mapper.Map<List<ContactDto>>(contacts);
                _logger.LogInformation("Consulta de contactos realizada correctamente. Total: {Count}", dto.Count);
                return Ok(new
                {
                    totalCount,
                    items = dto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de contactos");
                return StatusCode(500, $"Error al obtener la lista de contactos: {ex.Message}");
            }
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContact(int id)
        {
            try
            {
                var contact = await _context.Set<Contact>().FindAsync(id);
                if (contact == null)
                {
                    _logger.LogWarning("Contacto no encontrado para id {Id}", id);
                    return NotFound();
                }
                var dto = _mapper.Map<ContactDto>(contact);
                _logger.LogInformation("Consulta de contacto realizada correctamente para id {Id}", id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el contacto con id {Id}", id);
                return StatusCode(500, $"Error al obtener el contacto: {ex.Message}");
            }
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactDto contactDto)
        {
            if (contactDto == null)
            {
                _logger.LogWarning("Intento de creación de contacto fallido: DTO nulo");
                return BadRequest();
            }
            var contact = _mapper.Map<Contact>(contactDto);
            _context.Set<Contact>().Add(contact);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Contacto creado correctamente con id {Id}", contact.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el contacto");
                return StatusCode(500, $"Error al guardar el contacto: {ex.Message}");
            }
            var resultDto = _mapper.Map<ContactDto>(contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, resultDto);
        }
    }
}
