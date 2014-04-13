using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class UserDTO : IdentityDTO
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
    }
}
