using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Material
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        // Constructor

        public Material() { }

        public Material(int id, string nombre, string descripcion, decimal precio)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
        }

        // Método para mostrar información del material
        public override string ToString()
        {
            return $"{Nombre} - {Descripcion} - {Precio}";
        }
    }
}
