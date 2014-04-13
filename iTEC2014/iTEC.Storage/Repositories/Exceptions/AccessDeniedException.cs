using System;

namespace iTEC.Storage.Repositories.Exceptions
{
    public class AccessDeniedException : RepositoryException
    {
        public AccessDeniedException()
            : base("Access denied.")
        {
        }
    }
}