using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ApartmentId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, StringLength(1)]
        public string StatusId { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Campo opcional para almacenar el PDF del contrato en binario (hasta 10 MB)
        [MaxLength(10485760)]
        public byte[]? ContractPdf { get; set; }
    }
}