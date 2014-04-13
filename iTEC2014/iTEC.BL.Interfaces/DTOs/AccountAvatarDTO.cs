using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class AccountAvatarDTO : IdentityDTO
    {
        [Required]
        public String Username { get; set; }
        public Int32 Role { get; set; }
        [Required]
        public Byte[] Avatar { get; set; }
        public Boolean Remember { get; set; }
    }
}
