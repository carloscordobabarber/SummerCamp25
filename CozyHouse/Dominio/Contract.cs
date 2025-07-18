using System;

public class Contract
{
    private int contractId;
	private string clientDni;
    private string clientName;
    private string clientBankAccount;
    private int payment;
    private DateTime startDate;
    private DateTime endDate;
    private string apartmentAddress;

    public Contract(int contractId, string clientDni, string clientName, string clientBankAccount, int payment, DateTime startDate, DateTime endDate, string apartmentAddress)
    {
        this.ContractId = contractId;
        this.ClientDni = clientDni;
        this.ClientName = clientName;
        this.ClientBankAccount = clientBankAccount;
        this.Payment = payment;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.ApartmentAddress = apartmentAddress;
    }

    // Getters
    public int get ContractId()
    {
        return contractId;
    }
    public string get ClientDni()
    {
        return clientDni;
    }
    public string get ClientName()
    {
        return clientName;
    }
    public string get ClientBankAccount()
    {
        return clientBankAccount;
    }
    public int get Payment()
    {
        return payment;
    }
    public DateTime get StartDate()
    {
        return startDate;
    }
    public DateTime get EndDate()
    {
        return endDate;
    }
    public string get ApartmentAddress()
    {
        return apartmentAddress;
    }

    // Setters
    public void set ContractId(int contractId)
    {
        this.contractId = contractId;
    }
    public void set ClientDni(string clientDni)
    {
        this.clientDni = clientDni;
    }
    public void set ClientName(string clientName)
    {
        this.clientName = clientName;
    }
    public void set ClientBankAccount(string clientBankAccount)
    {
        this.clientBankAccount = clientBankAccount;
    }
    public void set Payment(int payment)
    {
        this.payment = payment;
    }
    public void set StartDate(DateTime startDate)
    {
        this.startDate = startDate;
    }
    public void set EndDate(DateTime endDate)
    {
        this.endDate = endDate;
    }
    public void set ApartmentAddress(string apartmentAddress)
    {
        this.apartmentAddress = apartmentAddress;
    }
}
