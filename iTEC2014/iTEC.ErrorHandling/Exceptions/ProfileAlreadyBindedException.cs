using System;

namespace iTEC.ErrorHandling.Exceptions
{
    public class ProfileAlreadyBindedException : RepositoryException
    {
        private String profile;

        public ProfileAlreadyBindedException(String name)
        {
            profile = name;
        }

        public override string Message
        {
            get
            {
                return String.Format("The game profile \"{0}\" is already binded.", profile);
            }
        }
    }
}