using iTEC.Storage.Entities;
using iTEC.Storage.Repositories.Contexts;
using iTEC.Storage.Repositories.DTO;
using iTEC.Storage.Repositories.Exceptions;
using System;
using System.IO;
using System.Linq;

namespace iTEC.Storage.Repositories
{
    public class AccountRepository : PagerRepository<PickPlaceContext>
    {
        public AccountAvatarDTO GetUserStateInfo(Int32 id)
        {
            var user = GetUser(id);

            var state = new AccountAvatarDTO();
            if (user != null)
            {
                state.Role = (Int32)user.Role;
                state.Username = user.Username;
                state.Avatar = user.Profile.Avatar;
                state.Remember = user.Remember;
            }

            return state;
        }

        public Int32 Authenticate(String username, String password, Boolean remember)
        {
            var user = GetUser(username, password);
            if (user == null)
            {
                throw new AuthenticationFailedException();
            }

            user.Remember = remember;
            Save();

            return user.Id;
        }

        public void RegisterUser(String username, String password, String email)
        {
            Register(username, password, email, Role.User);
        }

        public void RegisterOwner(String username, String password, String email)
        {
            Register(username, password, email, Role.Owner);
        }

        public Int32 GetAccountId(String username)
        {
            var user = GetUser(username);
            if (user == null)
            {
                throw new UsernameNotFoundException();
            }
            return user.Id;
        }

        #region Helpers

        protected Account GetUser(Int32 id)
        {
            return GetAll<Account>().FirstOrDefault(x => x.Id == id);
        }

        protected Account GetUser(String username)
        {
            return GetAll<Account>().FirstOrDefault(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }

        protected Account GetUser(String username, String password)
        {
            return GetAll<Account>().FirstOrDefault(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                x.Password.Equals(password, StringComparison.InvariantCultureIgnoreCase));
        }

        private void Register(String username, String password, String email, Role role)
        {
            var user = GetUser(username);
            if (user != null)
            {
                throw new UsernameAlreadyInUseException();
            }

            user = new Account()
            {
                Username = username,
                Password = password,
                Remember = true,
                Role = role,
                Profile = new Profile()
                {
                    Email = email,
                    Avatar = null
                }
            };

            Add<Account>(user);
            Save();
        }

        #endregion
    }
}