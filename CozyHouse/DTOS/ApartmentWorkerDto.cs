using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class ApartmentWorkerDto : ApartmentDTO
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsAvailable { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string StreetName { get; set; }
    }
}
