using Dominio;

namespace CrearDistrict
{
    class Program
    {
        static void Main(string[] args)
        {
            // Pedimos datos al usuario


            Console.WriteLine("Introduzca los datos para crear un distrito:");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nombre: ");
            string name = Console.ReadLine();
            Console.Write("Descripción: ");
            string description = Console.ReadLine();
            Console.Write("Punto de referencia: ");
            string landmark = Console.ReadLine();

            // Creamos el objeto District
            
            District district = new District(id, name, description, landmark);
            // Mostramos los datos
            Console.WriteLine("\nDatos del distrito:");
            Console.WriteLine($"ID: {district.getId()}");
            Console.WriteLine($"Nombre: {district.getName()}");
            Console.WriteLine($"Descripción: {district.getDescription()}");
            Console.WriteLine($"Punto de referencia: {district.getLandmark()}");
            Console.WriteLine("\nPresione una tecla para salir...");
            Console.ReadKey();

        }
    }
}