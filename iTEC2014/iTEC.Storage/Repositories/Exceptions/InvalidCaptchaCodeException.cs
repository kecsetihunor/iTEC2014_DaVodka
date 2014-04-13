using System;

namespace iTEC.Storage.Repositories.Exceptions
{
    public class InvalidCaptchaCodeException : RepositoryException
    {
        public InvalidCaptchaCodeException()
            : base("The provided captcha code is incorrect!")
        {
        }
    }
}