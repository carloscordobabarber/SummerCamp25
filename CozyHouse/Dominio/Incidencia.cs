using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Incidencia
    {
        // Propiedades automáticas
        public string OwnersName { get; set; }
        public string ClientName { get; set; }
        public string Direcction { get; set; }
        public string Incidence { get; set; }

        // Constructores
        public Incidencia() { }

        public Incidencia(string ownersName, string clientName, string direcction, string incidence)
        {
            OwnersName = ownersName;
            ClientName = clientName;
            Direcction = direcction;
            Incidence = incidence;
        }
    }
}
