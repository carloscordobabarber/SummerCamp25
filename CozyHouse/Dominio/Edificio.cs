using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Edificio
    {
        private string direcction;
        private int floor;
        private string door;

        public string Direcction { get; set; }

        public int Floor { get; set; }

        public string Door { get; set; }


        public Edificio() { }

        public Edificio(string direcction, int floor, string door)
        {
            this.Direcction = direcction;
            this.Floor = floor;
            this.Door = door;
        }


    }
}
