using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class VotedCategoryDTO : CategoryDTO
    {
        public Int32 Points { get; set; }
    }
}
