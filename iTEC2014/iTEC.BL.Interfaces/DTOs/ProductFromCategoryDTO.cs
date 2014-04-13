using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class ProductFromCategoryDTO : IdentityDTO
    {
        public string Name { get; set; }
        public bool InGroup { get; set; }
    }
}
