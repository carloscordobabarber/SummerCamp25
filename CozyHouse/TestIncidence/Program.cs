using Dominio;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Propietario de la vivienda: ");
        string ownersName = Console.ReadLine();

        Console.Write("Nombre del cliente: ");
        string clientName = Console.ReadLine();

        Console.Write("Dirección de la incidencia: ");
        string direcction = Console.ReadLine();

        Console.Write("Descripción de la incidencia: ");
        string incidence = Console.ReadLine();

        Incidence incidence1 = new Incidence(1, ownersName, clientName, direcction, incidence);
        Console.WriteLine($"\nNºincidencia: {incidence1.IdIncidence}");
        Console.WriteLine($"El propietario es: {incidence1.OwnersName}");
        Console.WriteLine($"Ha llamado el cliente: {incidence1.ClientName}");
        Console.WriteLine($"Ha habido la incidencia: {incidence1.TheIncidence}");

        Incidence incidence2 = new Incidence();

        incidence2.IdIncidence = 2;
        incidence2.OwnersName = "Juan Pérez";
        incidence2.ClientName = "Ana García";
        incidence2.Direcction = "Calle Falsa 123";
        incidence2.TheIncidence = "Fuga de agua en el baño";

        Console.WriteLine($"\nNºincidencia: {incidence2.IdIncidence}");
        Console.WriteLine($"El propietario es: {incidence2.OwnersName}");
        Console.WriteLine($"Ha llamado el cliente: {incidence2.ClientName}");
        Console.WriteLine($"Ha habido la incidencia: {incidence2.TheIncidence}");

    }
}