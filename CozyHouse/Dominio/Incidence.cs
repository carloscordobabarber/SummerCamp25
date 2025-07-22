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
        string incidence;
        string status;


        public int IdIncidence { get; set; }
        public string OwnersName { get; set; }
        public string ClientName { get; set; }
        public string Direcction { get; set; }
        public string TheIncidence { get; set; }

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

        public DateTime Date { get; set; }

        public Incidence() { }

        public Incidence(int idIncidence, string ownersName, string clientName, string direcction, string incidence, string status)
        {
            this.IdIncidence = idIncidence;
            this.OwnersName = ownersName;
            this.ClientName = clientName;
            this.Direcction = direcction;
            this.TheIncidence = incidence;
            this.Status = status;
        }

        public void UpdateStatus(string newStatus) {
            if (newStatus == "Pendiente" || newStatus == "En Proceso" || newStatus == "Resuelta") {
                this.Status = newStatus;
                if (newStatus == "Resuelta") {  this.Date = DateTime.Now; }
            } else {
                throw new ArgumentException("El estado debe ser 'Pendiente', 'En Proceso' o 'Resuelta'.");
            }
        }

        public void CloseIncidence() {
            this.Status = "Resuelta";
            this.Date = DateTime.Now;
        }

        public void AddIncidence(int idIncidence, string ownersName, string clientName, string direcction, string incidence, string status, DateTime incidenceDate)
        {
            this.IdIncidence = idIncidence;
            this.OwnersName = ownersName;
            this.ClientName = clientName;
            this.Direcction = direcction;
            this.TheIncidence = incidence;
            this.Status = status;
            this.Date = incidenceDate;
        }

        public void AssignTechnician(Incidence newIncidence) {
            if (newIncidence.Status == "Pendiente") {
                newIncidence.Status = "En Proceso";
            } else {
                throw new InvalidCastException("La incidencia no está pendiente.");
            }
        }
    }
}
