namespace SistemaAPI.Entidades;

public class Apartment {
    public int Id { get; set; }
    public string Door { get; set; } = string.Empty;
    public int Floor { get; set; }
    public double Price { get; set; }
    public double Area { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public bool IsAvailable { get; set; }

    // FK y navegación al edificio
    public int BuildingId { get; set; }
    public Building? Building { get; set; }

    // Relación con el alquiler
    public Rental? Rental { get; set; }
}
