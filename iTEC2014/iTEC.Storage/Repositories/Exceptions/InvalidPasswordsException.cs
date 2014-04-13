using System;

namespace iTEC.Storage.Repositories.Exceptions
{
    public class InvalidPasswordsException : RepositoryException
    {
        public InvalidPasswordsException()
            : base("The passwords does not match!")
        {
        }
    }
}