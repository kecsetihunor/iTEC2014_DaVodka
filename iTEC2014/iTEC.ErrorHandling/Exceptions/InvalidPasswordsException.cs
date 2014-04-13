using System;

namespace iTEC.ErrorHandling.Exceptions
{
    public class InvalidPasswordsException : RepositoryException
    {
        public InvalidPasswordsException()
            : base("The passwords does not match!")
        {
        }
    }
}