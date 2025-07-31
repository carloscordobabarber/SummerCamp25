namespace SistemaAPI.Entidades;

public class User {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // Relación: un usuario puede tener varios alquileres
    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
