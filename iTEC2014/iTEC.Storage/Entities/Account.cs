using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTEC.Storage.Entities
{
    public enum Role
    {
        Owner = 1,
        User = 2
    }

    [Table("Accounts")]
    public class Account
    {
        public Int32 Id { get; set; }

        public String Username { get; set; }

        public String Password { get; set; }

        public Boolean Remember { get; set; }

        public Role Role { get; set; }

        public virtual Profile Profile { get; set; }
    }
}