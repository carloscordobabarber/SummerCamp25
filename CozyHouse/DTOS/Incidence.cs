// Archivo: Incidence.cs
namespace DTOS;

public class Incidence {

    private string status = "Pendiente"; // Valor por defecto

    public int IdIncidence { get; set; }
    public string Tenant { get; set; } = string.Empty;
    public string Spokesperson { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CompanyIncidence { get; set; } = string.Empty;
    public string Status {
        get => status;
        set {
            if (value == "Pendiente" || value == "En Proceso" || value == "Resuelta") {
                status = value;
            } else {
                throw new ArgumentException("El estado debe ser 'Pendiente', 'En Proceso' o 'Resuelta'.");
            }
        }
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Incidence() { }

    public Incidence(int idIncidence, string tenant, string spokesperson, string direction, string description, string status, DateTime createdAt, DateTime updatedAt)
    {
        IdIncidence = idIncidence;
        Tenant = tenant;
        Spokesperson = spokesperson;
        Direction = direction;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    //TODO: verificar
    public void UpdateStatus(string newStatus) {
        if (newStatus == "Pendiente" || newStatus == "En Proceso" || newStatus == "Resuelta") {
            Status = newStatus;
            if (newStatus == "Resuelta") {  UpdatedAt = DateTime.Now; }
        } else {
            throw new ArgumentException("El estado debe ser 'Pendiente', 'En Proceso' o 'Resuelta'.");
        }
    }

    public void CloseIncidence() {
        Status = "Resuelta";
        UpdatedAt = DateTime.Now;
    }

    public void AssignTechnician() {
        if (Status == "Pendiente") {
            Status = "En Proceso";
        } else {
            throw new InvalidCastException("La incidencia no está pendiente.");
        }
    }
}
