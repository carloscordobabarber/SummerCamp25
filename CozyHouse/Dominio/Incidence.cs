using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Incidence
    {
        [Key]
        public int IdIncidence { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        [RegularExpression(@"^[\p{L}\s'-]+$", ErrorMessage = "El nombre tiene caracteres no válidos")]
        public string Tenant { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        [RegularExpression(@"^[\p{L}\s'-]+$", ErrorMessage = "El nombre tiene caracteres no válidos")]
        public string Spokesperson { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Direction { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(100)]
        public string CompanyIncidence { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // FK y navegación al apartamento
        public int ApartmentId { get; set; }
        // Relación con el alquiler
        public int RentalId { get; set; }
    }
}
