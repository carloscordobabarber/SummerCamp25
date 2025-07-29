using Microsoft.VisualBasic;
using System;
namespace Dominio;
public class Client
{
    public int Id { get; set; }
    public string Dni { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string BankAccount { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;

    public DateTime Birthday { get; set; }
    public string Email { get; set; }

    public Client() { }

    public Client(int id, string dni, string name, string bankAccount, string phoneNumber, string email,DateTime birthday)
    {
        Id = id;
        Dni = dni;
        Name = name;
        BankAccount = bankAccount;
        PhoneNumber = phoneNumber;
        Email = email;
        Birthday = birthday;
    }

    public Client(int id, string dni, string name, DateTime dateOfBirth, string bankAccount, string phoneNumber, string email)
    {
        Id = id;
        Dni = dni;
        Name = name;
        BirthDate = dateOfBirth;
        BankAccount = bankAccount;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}

