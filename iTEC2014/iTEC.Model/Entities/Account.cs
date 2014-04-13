using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Entities
{
    [Table("Accounts")]
    public class Account : AbstractIdentifiableEntity
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public Boolean Remember { get; set; }
        public Role Role { get; set; }
        public virtual UserGroup UserGroup { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
