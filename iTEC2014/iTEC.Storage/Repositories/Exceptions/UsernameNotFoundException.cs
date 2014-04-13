using System;

namespace iTEC.Storage.Repositories.Exceptions
{
    public class UsernameNotFoundException : RepositoryException
    {
        public UsernameNotFoundException()
            : base("Invalid user access.")
        {
        }
    }
}