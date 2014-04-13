using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iTEC.Storage.Repositories.DTO
{
    public interface IUser
    {
        [Required]
        String Username { get; set; }
    }

    public interface IAvatar
    {
        [Required]
        Byte[] Avatar { get; set; }
    }

    public class AccountAvatarDTO : IUser, IAvatar
    {
        public String Username { get; set; }
        public Int32 Role { get; set; }
        public Byte[] Avatar { get; set; }
        public Boolean Remember { get; set; }
    }
}
