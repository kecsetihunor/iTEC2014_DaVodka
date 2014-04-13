using System;

namespace iTEC.ErrorHandling.Exceptions
{
    public class AuthenticationFailedException : RepositoryException
    {
        public AuthenticationFailedException()
            : base("Invalid username or password.")
        {

        }
    }
}