using System.ComponentModel.DataAnnotations;

namespace Dominio;

public class Apartment {
    
    public Guid ApartmentId { get; set; } = Guid.NewGuid();

    [Key]

    public int Id { get; set; }

    [Required]
    [MaxLength(1)]
    [RegularExpression(@"^[A-Z]$", ErrorMessage = "El apartamento debe tener una letra de la A a la Z.")]
    public string Door { get; set; } = string.Empty;
    
    [Required]
    
    [Range(1, 99)]
    public int Floor { get; set; }
    [Required, Range(0, double.MaxValue)]
    public double Price { get; set; }
    [Required]
    [Range(1, 2000)]
    public double Area { get; set; }
    [Required]
    [Range(1, 50)]
    public int NumberOfRooms { get; set; }
    [Required]
    [Range(1, 50)]
    public int NumberOfBathrooms { get; set; }

    public bool IsAvailable { get; set; }
    // FK y navegación al edificio
    [Required]
    public int BuildingId { get; set; }
    //[Required]
    //public Building Building { get; set; }

    // Relación con el alquiler
    public int rentalId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
