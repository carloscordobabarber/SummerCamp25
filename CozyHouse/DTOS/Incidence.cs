// Archivo: Incidence.cs
namespace DTOS
{
    public class Incidence
    {
        public int IdIncidence { get; set; }
        public string OwnersName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string Direcction { get; set; } = string.Empty;
        public string TheIncidence { get; set; } = string.Empty;

        private string status = "Pendiente"; // Valor por defecto

        public string Status
        {
            get => status;
            set
            {
                if (value == "Pendiente" || value == "En Proceso" || value == "Resuelta")
                {
                    status = value;
                }
                else
                {
                    throw new ArgumentException("El estado debe ser 'Pendiente', 'En Proceso' o 'Resuelta'.");
                }
            }
        }

        public DateTime Date { get; set; } = DateTime.Now;

        public Incidence() { }

        public Incidence(int idIncidence, string ownersName, string clientName, string direcction, string incidence, string status)
        {
            IdIncidence = idIncidence;
            OwnersName = ownersName;
            ClientName = clientName;
            Direcction = direcction;
            TheIncidence = incidence;
            Status = status;
            Date = DateTime.Now;
        }

        public void UpdateStatus(string newStatus)
        {
            if (newStatus == "Pendiente" || newStatus == "En Proceso" || newStatus == "Resuelta")
            {
                Status = newStatus;
                if (newStatus == "Resuelta")
                {
                    Date = DateTime.Now;
                }
            }
            else
            {
                throw new ArgumentException("El estado debe ser 'Pendiente', 'En Proceso' o 'Resuelta'.");
            }
        }

        public void CloseIncidence()
        {
            Status = "Resuelta";
            Date = DateTime.Now;
        }

        public void AssignTechnician()
        {
            if (Status == "Pendiente")
            {
                Status = "En Proceso";
            }
            else
            {
                throw new InvalidCastException("La incidencia no está pendiente.");
            }
        }
    }
}
