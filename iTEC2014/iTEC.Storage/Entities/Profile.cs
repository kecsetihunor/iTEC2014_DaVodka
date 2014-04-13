using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTEC.Storage.Entities
{
    [Table("Profiles")]
    public class Profile
    {
        public Int32 Id { get; set; }

        public String Email { get; set; }

        public Byte[] Avatar { get; set; }
    }
}