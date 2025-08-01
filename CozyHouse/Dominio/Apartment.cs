using System.ComponentModel.DataAnnotations;

namespace Dominio;

public class Apartment {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MaxLength(1)]
    [RegularExpression(@"^[A-Z]$", ErrorMessage = "El apartamento debe tener una letra de la A a la Z.")]
    public string Door { get; set; } = string.Empty;
    [Required]
    [MaxLength(2)]
    [Range(1, 99)]
    public int Floor { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public double Price { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public double Area { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int NumberOfRooms { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int NumberOfBathrooms { get; set; }

    public bool IsAvailable { get; set; }
    // FK y navegación al edificio
    [Required]
    public Guid BuildingId { get; set; }
    [Required]
    public Building Building { get; set; }

    // Relación con el alquiler
    public Rental? Rental { get; set; }
}
