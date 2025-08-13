using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string Door { get; set; } = null!;

        [Required]
        public int Floor { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Area { get; set; }

        public int? NumberOfRooms { get; set; }

        public int? NumberOfBathrooms { get; set; }

        public bool? IsAvailable { get; set; }

        [Required]
        public int BuildingId { get; set; }

        public bool? HasLift { get; set; }

        public bool? HasGarage { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
