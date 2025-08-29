using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CozyData;
using DTOS;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersRentalsController : ControllerBase
    {
        private readonly ContextDataBase _context;

        public UsersRentalsController(ContextDataBase context)
        {
            _context = context;
        }

        // GET: api/UsersRentals/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserRentalDetailsDto>>> GetRentalsByUserId(int userId)
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

                result.Add(new UserRentalDetailsDto
                {
                    RentalId = rental.Id,
                    UserId = rental.UserId,
                    ApartmentId = rental.ApartmentId,
                    ApartmentPrice = apartment?.Price ?? 0,
                    ApartmentCode = apartment?.Code ?? string.Empty,
                    ApartmentDoor = apartment?.Door ?? string.Empty,
                    ApartmentFloor = apartment?.Floor ?? 0,
                    StartDate = rental.StartDate,
                    EndDate = rental.EndDate,
                    StatusId = rental.StatusId,
                    StatusName = status?.Name ?? string.Empty,
                    StreetName = street?.Name ?? string.Empty,
                    Portal = building?.Doorway ?? string.Empty,
                    Floor = apartment?.Floor ?? 0,
                    DistrictName = district?.Name ?? string.Empty
                });
            }

            return Ok(result);
        }
    }
}
