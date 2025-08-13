using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Status
    {
        [Key]
        [StringLength(1)]
        public string Id { get; set; } = null!;

        [Required, StringLength(10)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}