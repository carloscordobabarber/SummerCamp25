using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class ApartmentDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Door { get; set; } = null!;
        public int Floor { get; set; }
        public double Price { get; set; }
        public int Area { get; set; }
        public int? NumberOfRooms { get; set; }
        public int? NumberOfBathrooms { get; set; }
        
        public int BuildingId { get; set; }
        public bool HasLift { get; set; }
        public bool HasGarage { get; set; }

    }
}
