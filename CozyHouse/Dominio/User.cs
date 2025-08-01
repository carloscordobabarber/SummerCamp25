using System.ComponentModel.DataAnnotations;

namespace Dominio;

public class User {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MaxLength(100)]
    [MinLength(2)]
    // Validación: solo letras y espacios (\p{L} representa letras de cualquier idioma)
    [RegularExpression(@"^[\p{L}\s'-]+$", ErrorMessage = "El nombre tiene caracteres no válidos")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(200)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d\s]).{8,}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.")]
    public string Password { get; set; } = string.Empty;

    // Relación: un usuario puede tener varios alquileres
    public ICollection<Rental>? Rentals { get; set; } = new List<Rental>();
}
