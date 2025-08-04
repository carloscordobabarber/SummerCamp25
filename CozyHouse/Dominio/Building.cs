using System.ComponentModel.DataAnnotations;

namespace Dominio;

public class Building {
  
    public Guid Id { get; set; } = Guid.NewGuid();

    
    [Key]
    public int BuildingId { get; set; }
    
    [Required]
    [MaxLength(100)]

    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;



    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
