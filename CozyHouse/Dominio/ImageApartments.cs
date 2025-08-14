using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Images
    {
        [Key]
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        [Required]
        public byte[] Photo { get; set; }
    }
}
