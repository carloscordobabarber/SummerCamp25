using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class District
    {
        int id;
        string name;
        string description;
        string landmark;

        public District (int id, string name, string description, string landmark)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.landmark = landmark;
        }




    }
}