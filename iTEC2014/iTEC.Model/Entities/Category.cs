using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Entities
{
    [Table("Category")]
    public class Category : AbstractIdentifiableEntity
    {
        public String Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
