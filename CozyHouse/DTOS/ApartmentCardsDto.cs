using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class ApartmentCardsDto : ApartmentDTO
    {
        public bool IsAvailable { get; set; }
        public List<string>? ImageUrls { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string StreetName { get; set; }
    }
}
