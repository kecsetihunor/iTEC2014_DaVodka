using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Entities
{
    [Table("Profiles")]
    public class Profile : AbstractIdentifiableEntity
    {
        public String Email { get; set; }
        public Byte[] Avatar { get; set; }
    }
}
