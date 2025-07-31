using System.ComponentModel.DataAnnotations;

namespace SistemaAPI.Entidades;

public class Rental {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsConfirmed { get; set; }

    // Relación con usuario
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public User User { get; set; }

    // Relación con apartamento
    [Required]
    public Guid ApartmentId { get; set; }
    [Required]
    public Apartment Apartment { get; set; }
}
