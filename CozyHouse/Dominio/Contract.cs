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
        this.contractId = contractId;
        this.clientDni = clientDni;
        this.clientName = clientName;
        this.clientBankAccount = clientBankAccount;
        this.payment = payment;
        this.startDate = startDate;
        this.endDate = endDate;
        this.apartmentAddress = apartmentAddress;
    }

    // Métodos

    // Comprobar si el contrato está activo
    public bool IsActive()
    {
        DateTime today = DateTime.Today;
        return today >= startDate && today <= endDate;
    }

    // Renovar el contrato
    public void ExtendContract(DateTime newEndDate)
    {
        if (newEndDate > endDate)
        {
            endDate = newEndDate;
        }
    }

    // Getters y setters
    public int ContractId { get => contractId; set => contractId = value; }
    public string ClientDni { get => clientDni; set => clientDni = value; }
    public string ClientName { get => clientName; set => clientName = value; }
    public string ClientBankAccount { get => clientBankAccount; set => clientBankAccount = value; }
    public int Payment { get => payment; set => payment = value; }
    public DateTime StartDate { get => startDate; set => startDate = value; }
    public DateTime EndDate { get => endDate; set => endDate = value; }
    public string ApartmentAddress { get => apartmentAddress; set => apartmentAddress = value; }
}