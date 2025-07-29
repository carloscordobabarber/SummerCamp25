using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Log
    {
        public DateTime Fecha { get; set; }
        public string Accion { get; set; }
        public string Usuario { get; set; }
        public string Detalles { get; set; }
        public string Table { get; set; }

        public Log() { }
        public Log(DateTime fecha, string accion, string usuario, string detalles, string table)
        {
            Fecha = fecha;
            Accion = accion;
            Usuario = usuario;
            Detalles = detalles;
            Table = table;
        }

    }
}
