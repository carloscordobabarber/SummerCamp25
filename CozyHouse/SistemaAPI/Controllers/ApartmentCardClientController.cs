using Microsoft.AspNetCore.Mvc;
using CozyData;
using DTOS;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentCardClientController : ControllerBase
    {
        private readonly ContextDataBase _context;

        public ApartmentCardClientController(ContextDataBase context)
        {
            _context = context;
        }

        [HttpGet("UserApartments/{userId}")]
        public async Task<IActionResult> GetUserApartments(int userId)
        {
            var apartmentIds = await _context.Rentals
                .Where(r => r.UserId == userId)
                .Select(r => r.ApartmentId)
                .Distinct()
                .ToListAsync();

            if (apartmentIds == null || !apartmentIds.Any())
                return Ok(new List<ApartmentCardsDto>());

            var apartments = await _context.Apartments
                .Where(a => apartmentIds.Contains(a.Id))
                .AsNoTracking()
                .ToListAsync();
            var buildings = await _context.Buildings.AsNoTracking().ToListAsync();
            var districtStreets = await _context.DistrictStreets.AsNoTracking().ToListAsync();
            var districts = await _context.Districts.AsNoTracking().ToListAsync();
            var streets = await _context.Streets.AsNoTracking().ToListAsync();

            var apartmentDtos = new List<ApartmentCardsDto>();

            foreach (var apartment in apartments)
            {
                var dto = new ApartmentCardsDto
                {
                    Id = apartment.Id,
                    Code = apartment.Code,
                    Door = apartment.Door,
                    Floor = apartment.Floor,
                    Price = apartment.Price,
                    Area = apartment.Area,
                    NumberOfRooms = apartment.NumberOfRooms,
                    NumberOfBathrooms = apartment.NumberOfBathrooms,
                    BuildingId = apartment.BuildingId,
                    HasLift = apartment.HasLift,
                    HasGarage = apartment.HasGarage,
                    IsAvailable = apartment.IsAvailable
                };

                var building = buildings.FirstOrDefault(b => b.Id == apartment.BuildingId);
                string streetName = string.Empty;
                int dtoDistrictId = 0;
                string districtName = string.Empty;

                if (building != null)
                {
                    var street = streets.FirstOrDefault(s => s.Code == building.CodeStreet);
                    if (street != null)
                        streetName = street.Name;

                    var streetId = street?.Id ?? 0;
                    var districtStreet = districtStreets.FirstOrDefault(ds => ds.StreetId == streetId);
                    if (districtStreet != null)
                    {
                        var district = districts.FirstOrDefault(d => d.Id == districtStreet.DistrictId);
                        if (district != null)
                        {
                            dtoDistrictId = district.Id;
                            districtName = district.Name;
                        }
                    }
                }
                dto.StreetName = streetName;
                dto.DistrictId = dtoDistrictId;
                dto.DistrictName = districtName;

                dto.ImageUrls = await _context.imageApartments
                    .AsNoTracking()
                    .Where(img => img.ApartmentId == apartment.Id)
                    .Select(img => img.PhotoUrl)
                    .ToListAsync();

                apartmentDtos.Add(dto);
            }

            return Ok(apartmentDtos);
        }
    }
}
