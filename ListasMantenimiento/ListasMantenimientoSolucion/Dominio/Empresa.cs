using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        // Constructor

        public Empresa() { }
        
        public Empresa(int id, string nombre, string direccion, string telefono, string email)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
        }

        // Método para mostrar información de la empresa
        public override string ToString()
        {
            return $"{Nombre} - {Direccion} - {Telefono} - {Email}";
        }
    }
}
