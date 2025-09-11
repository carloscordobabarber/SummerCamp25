using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS
{
    public class UserRegisterDto : UserDto
    {
        public string Password { get; set; }
        public string? StatusId { get; set; } = null!;
    }
}
