using Dominio;

internal class Program
{
    private static void Main(string[] args)
    {
        // Pseudocódigo:
        // 1. Crear una instancia de la clase Incidence.
        // 2. Asignar valores a sus propiedades.
        // 3. Mostrar los valores por consola.
        // 4. Comprobar que los valores son correctos.


        Incidence incidence1 = new Incidence();
  

        incidence1.IdIncidence = 1;
        incidence1.OwnersName = "Juan Pérez";
        incidence1.ClientName = "Ana García";
        incidence1.Direcction = "Calle Falsa 123";
        incidence1.TheIncidence = "Fuga de agua en el baño";

        Console.WriteLine($"Nºincidencia: {incidence1.IdIncidence}");
        Console.WriteLine($"El propietario es: {incidence1.OwnersName}");
        Console.WriteLine($"Ha llamado el cliente: {incidence1.ClientName}");
        Console.WriteLine($"Ha habido la incidencia: {incidence1.TheIncidence}");

        //// Comprobación simple
        //if (incidence.Id == 1 && incidence.Description == "Prueba de incidencia" && incidence.IsActive)
        //{
        //    Console.WriteLine("La clase Incidence funciona correctamente.");
        //}
        //else
        //{
        //    Console.WriteLine("Error en la clase Incidence.");
        //}
    }
}