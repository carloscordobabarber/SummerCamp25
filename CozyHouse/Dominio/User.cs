using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(3), RegularExpression("^(?i)(dni|nie)$", ErrorMessage = "El tipo de documento debe ser 'dni' o 'nie'.")]
        public string DocumentType { get; set; } = null!;

        [Required, StringLength(9), RegularExpression(@"^(\d{8}[A-Z]|[XYZ]\d{7}[A-Z])$", ErrorMessage = "El tipo de documento debe ser un DNI o NIE v�lido.")]
        public string DocumentNumber { get; set; } = null!;

        [Required, StringLength(36), MinLength(2), RegularExpression(@"^[\p{L} \-'�]+$", ErrorMessage = "El nombre contiene caracteres inv�lidos.")]
        public string Name { get; set; } = null!;

        [Required, StringLength(36), RegularExpression(@"^[\p{L} \-'�]+$", ErrorMessage = "El nombre contiene caracteres inv�lidos.")]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(100), RegularExpression(@"^[a-zA-Z0-9._]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
    ErrorMessage = "El correo electr�nico no tiene un formato v�lido.")]
        public string Email { get; set; } = null!;

        [Required, StringLength(20)]
        public string Phone { get; set; }

        [Required, StringLength(24), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':\""\\|,.<>\/?]).{8,}$", ErrorMessage = "La contrase�a debe tener al menos 8 caracteres, incluyendo may�sculas, min�sculas, n�meros y un car�cter especial.")]
        public string Password { get; set; } = null!;

        [Required, StringLength(6)]
        public string Role { get; set; } = null!;

        [Required, StringLength(1)]
        public string StatusId { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}