using System.ComponentModel.DataAnnotations;

namespace Dominio;

public class Rental {
    [Key]
    public int RentalId { get; set; }

  
    
    public Guid Id { get; set; } = Guid.NewGuid();

    // Relación con usuario
    [Required]
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsConfirmed { get; set; }
    [Required]
    public int ApartmentId { get; set; }

  
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
