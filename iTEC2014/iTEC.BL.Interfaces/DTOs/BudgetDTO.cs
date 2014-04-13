using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.DTOs
{
    public class BudgetDTO
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public double Percentage { get; set; }
        public double Value { get; set; }
    }
}
