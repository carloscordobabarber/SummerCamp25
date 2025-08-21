using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100), RegularExpression(@"^[a-zA-Z0-9._]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get; set; } = null!;

        [Required, StringLength(100)]
        public string ContactReason { get; set; } = null!;

        [Required, StringLength(1000)]
        public string Message { get; set; } = null!;
    }
}
