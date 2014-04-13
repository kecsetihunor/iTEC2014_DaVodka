using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Entities
{
    [Table("Vote")]
    public class Vote : AbstractIdentifiableEntity
    {
        public Int32 Points { get; set; }
        public virtual Account Account { get; set; }
        public virtual Product Product { get; set; }
        public virtual Session Session { get; set; }
    }
}
