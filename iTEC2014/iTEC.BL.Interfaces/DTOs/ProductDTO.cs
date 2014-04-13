using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class ProductDTO : EntityDTO
    {
    }

    public class PricedProductDTO : ProductDTO
    {
        public Int32 Price { get; set; }
    }
}
