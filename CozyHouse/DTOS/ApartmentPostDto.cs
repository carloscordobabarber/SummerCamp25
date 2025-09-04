using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class ApartmentPostDto : ApartmentDTO
    {
        
       
        public bool IsAvailable { get; set; }

        public string StatusId { get; set; } 



    }
}
