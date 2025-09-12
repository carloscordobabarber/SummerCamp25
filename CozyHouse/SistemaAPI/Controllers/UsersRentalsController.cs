using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CozyData;
using DTOS;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersRentalsController : ControllerBase
    {
        private readonly ContextDataBase _context;
        private readonly ILogger<UsersRentalsController> _logger;

        public UsersRentalsController(ContextDataBase context, ILogger<UsersRentalsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/UsersRentals/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserRentalDetailsDto>>> GetRentalsByUserId(int userId)
        {
            try
            {
                var rentals = await _context.Rentals
                    .Where(r => r.UserId == userId)
                    .ToListAsync();

                var apartments = await _context.Apartments.ToListAsync();
                var statuses = await _context.Statuses.ToListAsync();
                var buildings = await _context.Buildings.ToListAsync();
                var streets = await _context.Streets.ToListAsync();
                var districtStreets = await _context.DistrictStreets.ToListAsync();
                var districts = await _context.Districts.ToListAsync();

                var result = new List<UserRentalDetailsDto>();

                foreach (var rental in rentals)
                {
                    var apartment = apartments.FirstOrDefault(a => a.Id == rental.ApartmentId);
                    var status = statuses.FirstOrDefault(s => s.Id == rental.StatusId);
                    var building = apartment != null ? buildings.FirstOrDefault(b => b.Id == apartment.BuildingId) : null;
                    var street = building != null ? streets.FirstOrDefault(s => s.Code == building.CodeStreet) : null;
                    var districtStreet = street != null ? districtStreets.FirstOrDefault(ds => ds.StreetId == street.Id) : null;
                    var district = districtStreet != null ? districts.FirstOrDefault(d => d.Id == districtStreet.DistrictId) : null;

                    // Obtener el último pago para este alquiler
                    var payment = await _context.Payments
                        .Where(p => p.RentalId == rental.Id)
                        .OrderByDescending(p => p.PaymentDate)
                        .FirstOrDefaultAsync();

                    // Obtener el nombre del estado de pago
                    string? paymentStatusName = null;
                    if (payment != null && payment.StatusId != null)
                    {
                        var paymentStatus = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == payment.StatusId);
                        paymentStatusName = paymentStatus?.Name;
                    }

                    result.Add(new UserRentalDetailsDto
                    {
                        RentalId = rental.Id,
                        UserId = rental.UserId,
                        ApartmentId = rental.ApartmentId,
                        ApartmentPrice = apartment?.Price ?? 0,
                        ApartmentCode = apartment?.Code ?? string.Empty,
                        ApartmentDoor = apartment?.Door ?? string.Empty,
                        ApartmentFloor = apartment?.Floor ?? 0,
                        ApartmentArea = apartment?.Area ?? 0,
                        StartDate = rental.StartDate,
                        EndDate = rental.EndDate,
                        StatusId = rental.StatusId,
                        StatusName = status?.Name ?? string.Empty,
                        StreetName = street?.Name ?? string.Empty,
                        Portal = building?.Doorway ?? string.Empty,
                        Floor = apartment?.Floor ?? 0,
                        DistrictName = district?.Name ?? string.Empty,
                        PaymentDate = payment?.PaymentDate,
                        PaymentStatusId = payment?.StatusId,
                        PaymentStatusName = paymentStatusName
                    });
                }

                _logger.LogInformation("Consulta de alquileres de usuario realizada correctamente para userId {UserId}. Total: {Count}", userId, result.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los alquileres del usuario con id {UserId}", userId);
                return StatusCode(500, $"Error al obtener los alquileres del usuario: {ex.Message}");
            }
        }

        // PATCH: api/UsersRentals/user/{rentalId}/status
        [HttpPatch("user/{rentalId}/status")]
        public async Task<IActionResult> PatchRentalStatus(int rentalId, [FromBody] PatchRentalStatusDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.StatusId))
            {
                _logger.LogWarning("PATCH alquiler fallido: StatusId nulo o vacío");
                return BadRequest("StatusId es obligatorio.");
            }

            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental == null)
            {
                _logger.LogWarning("PATCH alquiler fallido: alquiler id {RentalId} no encontrado", rentalId);
                return NotFound();
            }

            rental.StatusId = dto.StatusId;
            _context.Entry(rental).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Alquiler id {RentalId} actualizado correctamente a StatusId {StatusId}", rentalId, dto.StatusId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el StatusId del alquiler id {RentalId}", rentalId);
                return StatusCode(500, $"Error al actualizar el StatusId del alquiler: {ex.Message}");
            }

            return NoContent();
        }
    }
}
