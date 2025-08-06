using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class District
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();


        [Key]
        public int DistrictId { get; set; }

        [Required]
        [MaxLength(100)]

        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string description { get; set; } = string.Empty;

        public string? Landmark { get; set; } = string.Empty;




        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
