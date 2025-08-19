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
    public class ImagesController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;

        public ImagesController(ContextDataBase context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagesDto>>> GetImages()
        {
            var images = await _context.imageApartments.ToListAsync();
            var dto = _mapper.Map<List<ImagesDto>>(images);
            return Ok(dto);
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagesDto>> GetImage(int id)
        {
            var image = await _context.imageApartments.FindAsync(id);
            if (image == null)
                return NotFound();
            var dto = _mapper.Map<ImagesDto>(image);
            return Ok(dto);
        }

        // POST: api/Images
        [HttpPost]
        public async Task<IActionResult> PostImage([FromBody] ImagesDto imagesDto)
        {
            if (imagesDto == null)
                return BadRequest();
            var image = _mapper.Map<ImageApartment>(imagesDto);
            _context.imageApartments.Add(image);
            await _context.SaveChangesAsync();
            var resultDto = _mapper.Map<ImagesDto>(image);
            return CreatedAtAction(nameof(GetImage), new { id = image.Id }, resultDto);
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, [FromBody] ImagesDto imagesDto)
        {
            if (imagesDto == null || id != imagesDto.Id)
                return BadRequest();
            var image = await _context.imageApartments.FindAsync(id);
            if (image == null)
                return NotFound();
            image.ApartmentId = imagesDto.ApartmentId;
            image.PhotoUrl = imagesDto.PhotoUrl;
            image.PhotoDescription = imagesDto.PhotoDescription;
            _context.Entry(image).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.imageApartments.FindAsync(id);
            if (image == null)
                return NotFound();
            _context.imageApartments.Remove(image);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
