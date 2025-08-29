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
    public class ContactsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ContactsController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<object>> GetContacts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? contactReason = null)
        {
            var contactsQuery = _context.Set<Contact>().AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(contactReason))
                contactsQuery = contactsQuery.Where(c => c.ContactReason == contactReason);

            var totalCount = await contactsQuery.CountAsync();

            var contacts = await contactsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dto = _mapper.Map<List<ContactDto>>(contacts);
            return Ok(new
            {
                totalCount,
                items = dto
            });
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContact(int id)
        {
            var contact = await _context.Set<Contact>().FindAsync(id);
            if (contact == null)
                return NotFound();
            var dto = _mapper.Map<ContactDto>(contact);
            return Ok(dto);
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactDto contactDto)
        {
            if (contactDto == null)
                return BadRequest();
            var contact = _mapper.Map<Contact>(contactDto);
            _context.Set<Contact>().Add(contact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el contacto: {ex.Message}");
            }
            var resultDto = _mapper.Map<ContactDto>(contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, resultDto);
        }
    }
}
