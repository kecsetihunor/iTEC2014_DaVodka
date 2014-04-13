using System;

namespace iTEC.Storage.Repositories.Exceptions
{
    public class AuthenticationFailedException : RepositoryException
    {
        public AuthenticationFailedException()
            : base("Invalid username or password.")
        {

        }
    }
}