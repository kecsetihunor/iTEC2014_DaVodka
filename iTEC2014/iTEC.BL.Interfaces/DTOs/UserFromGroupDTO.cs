using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class UserFromGroupDTO : IdentityDTO
    {
        public string Username { get; set; }
        public bool InGroup { get; set; }
    }
}
