using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class VotedProductDTO : PricedProductDTO
    {
        public Int32 Points { get; set; }
    }
}
