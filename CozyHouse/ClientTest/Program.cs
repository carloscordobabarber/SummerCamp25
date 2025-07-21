string dni;
string name;
string bankAccount;
string phoneNumber;

Console.Write("Ingresa el dni: ");
dni = Console.ReadLine();

Console.Write("Ingresa el nombre del cliente: ");
name = Console.ReadLine();

Console.Write("Ingresa el número de cuenta: ");
bankAccount = Console.ReadLine();

Console.Write("Ingresa el número de teléfono: ");
phoneNumber = Console.ReadLine();

Client client= new Client(1, dni, name, bankAccount, phoneNumber);

Console.WriteLine(client.GetDni() + ", " + client.GetName() + ", " + client.GetBankAccount() + ", " + client.GetPhoneNumber());

