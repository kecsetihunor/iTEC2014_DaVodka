using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class VoteDTO : IdentityDTO
    {
        public Int32 Points { get; set; }
        public AccountAvatarDTO Account { get; set; }
        public PricedProductDTO Product { get; set; }
    }
}
