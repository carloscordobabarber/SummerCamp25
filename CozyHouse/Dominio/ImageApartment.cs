using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class ImageApartment
    {
        [Key]
        public int Id { get; set; }

        public int ApartmentId { get; set; }

        [Required]
        [StringLength(512)]
        public string PhotoUrl { get; set; } = null!;

        public string? PhotoDescription { get; set; }
    }
}
