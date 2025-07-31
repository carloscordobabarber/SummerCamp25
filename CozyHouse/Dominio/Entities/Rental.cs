namespace SistemaAPI.Entidades;

public class Rental {
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsConfirmed { get; set; }

    // Relación con usuario
    public int UserId { get; set; }
    public User? User { get; set; }

    // Relación con apartamento
    public int ApartmentId { get; set; }
    public Apartment? Apartment { get; set; }
}
