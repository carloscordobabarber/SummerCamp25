using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class ApartmentQPDto : ApartmentDTO
    {
     
        public string BuildingCode { get; set; }
        public bool HasLift { get; set; }
        public bool HasGarage { get; set; }
    }
}
