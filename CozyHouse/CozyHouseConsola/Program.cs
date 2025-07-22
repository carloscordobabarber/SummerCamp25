using Dominio;

Console.WriteLine("Welcome to CozyHouseConsola!");
 Client client = new Client(1, "464798", "John Doe", "1234567890","65682");
Console.WriteLine($"Client ID: {client.GetId()}");
//comentario