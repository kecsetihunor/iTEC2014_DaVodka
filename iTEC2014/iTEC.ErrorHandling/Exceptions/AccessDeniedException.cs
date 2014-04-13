using System;

namespace iTEC.ErrorHandling.Exceptions
{
    public class AccessDeniedException : RepositoryException
    {
        public AccessDeniedException()
            : base("Access denied.")
        {
        }
    }
}