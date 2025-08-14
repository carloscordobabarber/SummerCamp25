using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    
    public class DistrictStreet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DistrictId { get; set; }

        [Required]
        public int StreetId { get; set; }
    }
}