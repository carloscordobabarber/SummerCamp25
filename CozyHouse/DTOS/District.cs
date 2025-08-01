using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOS
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

        public int getId() { return id; }
        public string getName() { return name; }
        public string getDescription() { return description; }
        public string getLandmark() { return landmark; }






    }
}