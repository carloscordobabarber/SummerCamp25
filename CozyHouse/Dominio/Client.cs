using System;

public class Client
{
	private string dni;
	private string name;
	private string bankAccount;
	private string phoneNumber;

	public Client(string dni, string name, string bankAccount, string phoneNumber)
	{
        this.dni = dni;
        this.name = name;
        this.bankAccount = bankAccount;
        this.phoneNumber = phoneNumber;
    }

    // Getters
	public string GetDni()
    {
        return dni;
    }
    public string GetName()
    {
        return name;
    }
    public string GetBankAccount()
    {
        return bankAccount;
    }
    public string GetPhoneNumber()
    {
        return phoneNumber;
    }

    // Setters
    public void SetDni(string dni)
    {
        this.dni = dni;
    }
    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetBankAccount(string bankAccount)
    {
        this.bankAccount = bankAccount;
    }
    public void SetPhoneNumber(string phoneNumber)
    {
        this.phoneNumber = phoneNumber;
    }
}
