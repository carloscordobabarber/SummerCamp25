// Archivo: Contract.cs
namespace DTOS
{
    public class Contract
    {
        public int ContractId { get; set; }
        public string ClientDni { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientBankAccount { get; set; } = string.Empty;
        public int Payment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ApartmentAddress { get; set; } = string.Empty;
        //public Apartment Apartment { get; set; } = new Apartment(0, string.Empty, 0, 0.0, 0, 0, 0.0, true, "",int idBuilding);
       
        public int IdBuilding { get; set; }
        public int IdApartment { get; set; }

        public Contract() { }

        public Contract(int contractId, string clientDni, string clientName, string clientBankAccount, int payment, DateTime startDate, DateTime endDate, string apartmentAddress, int idApartment, int idBuilding)
        {
            ContractId = contractId;
            ClientDni = clientDni;
            ClientName = clientName;
            ClientBankAccount = clientBankAccount;
            Payment = payment;
            StartDate = startDate;
            EndDate = endDate;
            ApartmentAddress = apartmentAddress;
            IdApartment = idApartment;
            IdBuilding = idBuilding;
        }

        public bool IsActive()
        {
            DateTime today = DateTime.Today;
            return today >= StartDate && today <= EndDate;
        }

        public void ExtendContract(DateTime newEndDate)
        {
            if (newEndDate > EndDate)
            {
                EndDate = newEndDate;
            }
        }
    }
}
