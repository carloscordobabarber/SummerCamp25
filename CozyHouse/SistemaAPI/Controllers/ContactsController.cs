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
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts()
        {
            var contacts = await _context.Set<Contact>().ToListAsync();
            var dto = _mapper.Map<List<ContactDto>>(contacts);
            return Ok(dto);
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
            await _context.SaveChangesAsync();
            var resultDto = _mapper.Map<ContactDto>(contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, resultDto);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, [FromBody] ContactDto contactDto)
        {
            if (contactDto == null || id != contactDto.Id)
                return BadRequest();
            var contact = await _context.Set<Contact>().FindAsync(id);
            if (contact == null)
                return NotFound();
            contact.Email = contactDto.Email;
            contact.ContactReason = contactDto.ContactReason;
            contact.Message = contactDto.Message;
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Set<Contact>().FindAsync(id);
            if (contact == null)
                return NotFound();
            _context.Set<Contact>().Remove(contact);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
