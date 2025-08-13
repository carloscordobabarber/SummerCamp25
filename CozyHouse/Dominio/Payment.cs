using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(1)]
        public string StatusId { get; set; } = null!;

        [Required]
        public double Amount { get; set; }

        [Required]
        public int RentalId { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required, StringLength(24)]
        public string BankAccount { get; set; } = null!;
    }
}