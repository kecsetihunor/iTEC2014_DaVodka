using System;

namespace iTEC.ErrorHandling.Exceptions
{
    public class UsernameNotFoundException : RepositoryException
    {
        public UsernameNotFoundException()
            : base("Invalid user access.")
        {
        }
    }
}