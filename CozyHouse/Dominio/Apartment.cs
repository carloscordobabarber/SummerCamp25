using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Code { get; set; } = null!;

        [Required, StringLength(10)]
        public string Door { get; set; } = null!;

        [Required, Range(-1, 100)]
        public int Floor { get; set; }

        [Required, Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Area { get; set; }

        [Required, Range(0, 20)]
        public int NumberOfRooms { get; set; }

        [Required, Range(0, 20)]
        public int NumberOfBathrooms { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public int BuildingId { get; set; }

        [Required]
        public bool HasLift { get; set; }

        [Required]
        public bool HasGarage { get; set; }

        [Required, StringLength(1)]
        public string StatusId { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
