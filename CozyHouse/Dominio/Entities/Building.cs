using System.ComponentModel.DataAnnotations;

namespace SistemaAPI.Entidades;

public class Building {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;

    // Relación: un edificio tiene varios apartamentos
    public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}
