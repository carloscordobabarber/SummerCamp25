namespace DTOS
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string StatusId { get; set; } = null!;
        public double Amount { get; set; }
        public int RentalId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string BankAccount { get; set; } = null!;
    }
}