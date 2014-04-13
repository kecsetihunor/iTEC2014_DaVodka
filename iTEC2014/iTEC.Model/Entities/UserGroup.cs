using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Entities
{
    [Table("UserGroup")]
    public class UserGroup : AbstractIdentifiableEntity
    {
        public String Name { get; set; }
        public Int32 Points { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
