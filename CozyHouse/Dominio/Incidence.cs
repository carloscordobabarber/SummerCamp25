using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Incidence

    {
        int idIncidence;
        string ownersName;
        string clientName;
        string direcction;
        DateTime creationDate;
        DateTime resolutionDate;
        string description;
        string status;
        string responsable;
        string incidencePriority;


        public int IdIncidence { get; set; }
        public string OwnersName { get; set; }
        public string ClientName { get; set; }
        public string Direcction { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ResolutionDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Description { get; set; }  
        public string Status
        {
            get { return status; }
            set
            {
                if (value == "Pendiente" || value == "En Proceso" || value == "Resuelta") {
                    status = value;
                } else {
                    throw new ArgumentException("El estado debe ser 'Pendiente', 'En Proceso' o 'Resuelta'.");
                }
            }
        }

        public string Responsable { get; set; }
        public Priority IncidencePriority { get; set; }
        public enum Priority
        {
            Baja,
            Media,
            Alta,
            Urgente
        }

        public Incidence() { }

        public Incidence(int idIncidence, string ownersName, string clientName, string direcction, DateTime creationDate, DateTime resolutionDate, string description, string status, string responsable, Priority incidencePriority)
        {
            IdIncidence = idIncidence;
            OwnersName = ownersName;
            ClientName = clientName;
            Direcction = direcction;
            CreationDate = creationDate;
            ResolutionDate = resolutionDate;
            Description = description;
            Status = status;
            Responsable = responsable;
            IncidencePriority = incidencePriority;
        }

        public void UpdateStatus(string newStatus) {
            if (newStatus == "Pendiente" || newStatus == "En Proceso" || newStatus == "Resuelta") {
                this.Status = newStatus;
                if (newStatus == "Resuelta") {  this.LastUpdateDate = DateTime.Now; }
            } else {
                throw new ArgumentException("El estado debe ser 'Pendiente', 'En Proceso' o 'Resuelta'.");
            }
        }

        public void CloseIncidence() {
            this.Status = "Resuelta";
            this.ResolutionDate = DateTime.Now;
        }

        public void AddIncidence(int idIncidence, string ownersName, string clientName, string direcction, string description, string status, DateTime incidenceDate)
        {
            this.IdIncidence = idIncidence;
            this.OwnersName = ownersName;
            this.ClientName = clientName;
            this.Direcction = direcction;
            this.Description = description;
            this.Status = status;
            this.CreationDate = incidenceDate;
        }

        public void AssignTechnician(Incidence newIncidence, string responsable) {
            newIncidence.Responsable = responsable;
            newIncidence.LastUpdateDate = DateTime.Now;
            if (newIncidence.Status == "Pendiente") {
                newIncidence.Status = "En Proceso";
            } else {
                throw new InvalidCastException("La incidencia no está pendiente.");
            }
        }
    }
}
