using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Building
    {
        public int IdBuilding { get; set; }
        public string Direcction { get; set; }
        public int Floor { get; set; }
        public string Door { get; set; }


        public Building() { }

        public Building(int idBuilding, string direcction, int floor, string door)
        {
            this.IdBuilding = idBuilding;
            this.Direcction = direcction;
            this.Floor = floor;
            this.Door = door;
        }
    }
}
