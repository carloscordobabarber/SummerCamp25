using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado {
        public int id;
        public string nombre;
        public string apellido;
        public string email;
        public string telefono;
        public string direccion;
        public DateTime fechaNacimiento;
        public string puesto;

        public Empleado() { }

        public Empleado(int id, string nombre, string apellido, string email, string telefono, string direccion, DateTime fechaNacimiento, string puesto) {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.email = email;
            this.telefono = telefono;
            this.direccion = direccion;
            this.fechaNacimiento = fechaNacimiento;
            this.puesto = puesto;
        }

        public override string ToString() {
            return $"{id} - {nombre} {apellido} - {email} - {telefono} - {direccion} - {fechaNacimiento.ToShortDateString()} - {puesto}";
        }


    }
}
