using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class District
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; } = null!;

        [Required, StringLength(255)]
        public string Zipcode { get; set; } = null!;

        [Required, StringLength(255)]
        public string Country { get; set; } = null!;

        [Required, StringLength(255)]
        public string City { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}