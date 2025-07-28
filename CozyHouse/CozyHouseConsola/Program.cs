using Dominio;

// Archivo: Program.cs
using System;

namespace CozyHouseAlquiler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MostrarLogo();
            await MostrarMenu();
        }

        static void MostrarLogo()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  ____ ___ _______   __  _   _  ___  _   _ ____  _____ 
 / ___/ _ \__  /\ \ / / | | | |/ _ \| | | / ___|| ____|
| |  | | | |/ /  \ V /  | |_| | | | | | | \___ \|  _|  
| |__| |_| / /_   | |   |  _  | |_| | |_| |___) | |___ 
 \____\___/____|  |_|   |_| |_|\___/ \___/|____/|_____|
 _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|_|
            ");
            Console.ResetColor();
        }

        static async Task MostrarMenu()
        {
            int opcion;

            do
            {
                Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Introducir cliente");
                Console.WriteLine("2. Listar pisos");
                Console.WriteLine("3. Alquilar piso");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción (1-4): ");

                string entrada = Console.ReadLine();
                bool esNumero = int.TryParse(entrada, out opcion);

                if (!esNumero)
                {
                    Console.WriteLine("⚠️ Por favor, introduzca un número válido.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("→ Funcionalidad: Introducir cliente");
                        break;
                    case 2:
                        await Apartment.ShowApartmentsList();


                        break;
                    case 3:
                        Console.WriteLine("→ Funcionalidad: Alquilar piso");
                        break;
                    case 4:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("⚠️ Opción no válida. Intente de nuevo.");
                        break;
                }

            } while (opcion != 4);
        }
    }
}
