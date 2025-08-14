using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    
    public class DistrictStreet
    {
        [Key]
        public int Id { get; set; }

        public int DistrictId { get; set; }

       
        public int StreetId { get; set; }
    }
}