using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Entities
{
    [Table("Session")]
    public class Session : AbstractIdentifiableEntity
    {
        [Column(TypeName = "DateTime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime EndDate { get; set; }
    }
}
