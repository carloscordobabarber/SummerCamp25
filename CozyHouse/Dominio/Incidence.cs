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
        
        public int IdIncidence { get; set; }
        public string OwnersName { get; set; }
        public string ClientName { get; set; }
        public string Direcction { get; set; }
        public string TheIncidence { get; set; }

        public Incidence() { }

        public Incidence(int idIncidence, string ownersName, string clientName, string direcction, string incidence)
        {
            this.IdIncidence = idIncidence;
            this.OwnersName = ownersName;
            this.ClientName = clientName;
            this.Direcction = direcction;
            this.TheIncidence = incidence;
        }
    }
}
