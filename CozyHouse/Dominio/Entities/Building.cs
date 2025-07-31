namespace SistemaAPI.Entidades;

public class Building {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    // Relación: un edificio tiene varios apartamentos
    public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}
