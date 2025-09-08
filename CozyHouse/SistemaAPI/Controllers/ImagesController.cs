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
    public class ImagesController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(ContextDataBase context, IMapper mapper, ILogger<ImagesController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagesDto>>> GetImages()
        {
            try
            {
                var images = await _context.imageApartments.AsNoTracking().ToListAsync();
                var dto = _mapper.Map<List<ImagesDto>>(images);
                _logger.LogInformation("Consulta de imágenes realizada correctamente. Total: {Count}", dto.Count);
                return Ok(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de imágenes");
                return StatusCode(500, $"Error al obtener la lista de imágenes: {ex.Message}");
            }
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagesDto>> GetImage(int id)
        {
            try
            {
                var image = await _context.imageApartments.FindAsync(id);
                if (image == null)
                {
                    _logger.LogWarning("Imagen no encontrada para id {Id}", id);
                    return NotFound();
                }
                var dto = _mapper.Map<ImagesDto>(image);
                _logger.LogInformation("Consulta de imagen realizada correctamente para id {Id}", id);
                return Ok(dto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la imagen con id {Id}", id);
                return StatusCode(500, $"Error al obtener la imagen: {ex.Message}");
            }
        }

        // POST: api/Images
        [HttpPost]
        public async Task<IActionResult> PostImage([FromBody] ImagesDto imagesDto)
        {
            if (imagesDto == null)
            {
                _logger.LogWarning("Intento de creación de imagen fallido: DTO nulo");
                return BadRequest();
            }
            var image = _mapper.Map<ImageApartment>(imagesDto);
            _context.imageApartments.Add(image);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Imagen creada correctamente con id {Id}", image.Id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la imagen");
                return StatusCode(500, $"Error al guardar la imagen: {ex.Message}");
            }
            var resultDto = _mapper.Map<ImagesDto>(image);
            return CreatedAtAction(nameof(GetImage), new { id = image.Id }, resultDto);
        }
    }
}
