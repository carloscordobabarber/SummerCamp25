using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Street
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(24)]
        public string Code { get; set; } = null!;

        [Required, StringLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}