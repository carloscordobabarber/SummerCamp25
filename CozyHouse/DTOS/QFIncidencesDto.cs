using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class QFIncidencesDto
    {
        public string? AssignedCompany { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string StatusId { get; set; } = null!;
    }
}
