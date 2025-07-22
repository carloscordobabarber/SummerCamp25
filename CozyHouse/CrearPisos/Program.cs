// Archivo: Program.cs

using Dominio;

namespace CrearPisos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Pedimos datos al usuario
            Console.WriteLine("Introduce los datos del piso:");

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Dirección: ");
            string address = Console.ReadLine();

            Console.Write("Número de habitaciones: ");
            int numberOfRooms = int.Parse(Console.ReadLine());

            Console.Write("Precio: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Planta: ");
            int floor = int.Parse(Console.ReadLine());

            Console.Write("Número de baños: ");
            int numberOfBathrooms = int.Parse(Console.ReadLine());

            Console.Write("Área (m2): ");
            double area = double.Parse(Console.ReadLine());

            // Creamos el objeto Apartment
            Apartment apt = new Apartment(id, address, numberOfRooms, price, floor, numberOfBathrooms, area);

            // Mostramos los datos
            Console.WriteLine("\nDatos del piso:");
            Console.WriteLine($"ID: {apt.IdProperty}");
            Console.WriteLine($"Dirección: {apt.AddressProperty}");
            Console.WriteLine($"Número de habitaciones: {apt.NumberOfRoomsProperty}");
            Console.WriteLine($"Precio: {apt.PriceProperty} €");
            Console.WriteLine($"Planta: {apt.FloorProperty}");
            Console.WriteLine($"Número de baños: {apt.NumberOfBathroomsProperty}");
            Console.WriteLine($"Área: {apt.AreaProperty} m2");

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }
    }
}
