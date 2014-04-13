using iTEC.WebApplication.Captcha.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace iTEC.WebApplication.User.Models
{
    public interface IUser
    {
        [Required]
        String Username { get; set; }
    }

    public interface IRememberUser : IUser
    {
        Boolean Remember { get; set; }
    }

    public class User : IUser
    {
        public String Username { get; set; }

        public User()
        {
        }

        public User(IUser model)
        {
            Username = model.Username;
        }
    }

    public class RememberUser : User, IRememberUser
    {
        public Boolean Remember { get; set; }

        public RememberUser()
        {
        }

        public RememberUser(RememberUser model)
            : base(model)
        {
            Remember = model.Remember;
        }
    }

    public class UserModel : RememberUser
    {
        public Boolean IsAuthenticated { get; set; }

        public String Avatar { get; set; }

        public Int32 Role { get; set; }

        public UserModel()
        {
        }

        public UserModel(UserModel model)
            : base(model)
        {
            IsAuthenticated = model.IsAuthenticated;
            Avatar = model.Avatar;
        }
    }

    public class Account : User
    {
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }

    public class AuthenticationAccountModel : Account
    {
        public Boolean Remember { get; set; }
    }

    public class RegistrationAccountModel : Account
    {
        [Required]
        [DataType(DataType.Password)]
        public String Confirm { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        public CaptchaTextModel Captcha { get; set; }

        public String Type { get; set; }
    }
}