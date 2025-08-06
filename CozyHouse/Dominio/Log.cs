using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Log
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        [Key]
        public int LogID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required, MaxLength(50)]
        public string ActionPerformed { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string User { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string TableAffected { get; set; } = string.Empty;

        [Required, MaxLength(100)]
       public string Description { get; set; } = string.Empty;
      
       
          }
}
