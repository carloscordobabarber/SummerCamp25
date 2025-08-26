using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Incidence
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string Spokesperson { get; set; } = null!;

        [Required, StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public int IssueType { get; set; }

        [StringLength(100)]
        public string? AssignedCompany { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public int ApartmentId { get; set; }

        [Required]
        public int RentalId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [Required, StringLength(1)]
        public string StatusId { get; set; } = null!;
    }
}