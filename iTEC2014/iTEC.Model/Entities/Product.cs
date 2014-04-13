using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Entities
{
    [Table("Product")]
    public class Product : AbstractIdentifiableEntity
    {
        public String Name { get; set; }
        public Int32 Price { get; set; }
        public virtual Category Category { get; set; }  
    }
}
