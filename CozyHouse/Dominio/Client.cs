using System;
namespace Dominio;
public class Client
{
    public int Id { get; set; }
    public string Dni { get; set; }
    public string Name { get; set; }
    public string BankAccount { get; set; }
    public string PhoneNumber { get; set; }

    public Client() { }

    public Client(int id, string dni, string name, string bankAccount, string phoneNumber)
    {
        Id = id;
        Dni = dni;
        Name = name;
        BankAccount = bankAccount;
        PhoneNumber = phoneNumber;
    }
}

