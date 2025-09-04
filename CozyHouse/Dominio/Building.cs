using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Building
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string CodeBuilding { get; set; } = null!;

        [Required, StringLength(24)]
        public string CodeStreet { get; set; } = null!;

        [StringLength(255)]
        public string? Name { get; set; }

        [Required, StringLength(36)]
        public string Doorway { get; set; } = null!;

        [Required, StringLength(1)]
        public string StatusId { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}