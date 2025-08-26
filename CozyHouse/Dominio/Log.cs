using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required, StringLength(50)]
        public string ActionPerformed { get; set; } = null!;

        [Required]
        public int UsersId { get; set; }

        [Required, StringLength(50)]
        public string TableAffected { get; set; } = null!;

        [Required, StringLength(100)]
        public string Description { get; set; } = null!;
    }
}