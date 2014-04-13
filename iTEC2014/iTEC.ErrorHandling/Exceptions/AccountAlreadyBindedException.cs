using System;

namespace iTEC.ErrorHandling.Exceptions
{
    public class AccountAlreadyBindedException : RepositoryException
    {
        private String username;

        public AccountAlreadyBindedException(String user)
        {
            username = user;
        }

        public override string Message
        {
            get
            {
                return String.Format("The game account \"{0}\" is already binded.", username);
            }
        }
    }
}