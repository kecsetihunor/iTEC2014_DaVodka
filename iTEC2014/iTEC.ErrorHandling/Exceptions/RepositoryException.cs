using System;
using System.Runtime.Serialization;

namespace iTEC.ErrorHandling.Exceptions
{
    public abstract class RepositoryException : Exception
    {
        public RepositoryException()
            : base()
        {
        }

        public RepositoryException(String message)
            : base(message)
        {
        }

        public RepositoryException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        public RepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}