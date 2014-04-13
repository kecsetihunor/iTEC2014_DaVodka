using System;

namespace iTEC.ErrorHandling.Exceptions
{
    public class InvalidCaptchaCodeException : RepositoryException
    {
        public InvalidCaptchaCodeException()
            : base("The provided captcha code is incorrect!")
        {
        }
    }
}