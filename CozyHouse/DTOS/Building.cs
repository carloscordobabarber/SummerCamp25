using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTOS
{
    public class Building
    {
       
        public int IdBuilding { get; set; }
        public string Direcction { get; set; }

        //public string Name { get; set; }
      


        public Building() { }

        public Building(int idBuilding, string direcction)
        {
            IdBuilding = idBuilding;
            Direcction = direcction;
        }
    }
}
