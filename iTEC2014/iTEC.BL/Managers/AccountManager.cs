using iTEC.BL.Interfaces.DTOs;
using iTEC.BL.Interfaces.Managers;
using iTEC.ErrorHandling.Exceptions;
using iTEC.Model.Entities;
using iTEC.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Managers
{
    public class AccountManager : IAccountManager
    {
        private IUnitOfWork _unitOfWork;

        public AccountManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AccountAvatarDTO> GetAccounts()
        {
            return _unitOfWork.AccountRepository.Read()
                .Select(x => new AccountAvatarDTO
                {
                    Avatar = x.Profile.Avatar,
                    Remember = x.Remember,
                    Role = (int)x.Role,
                    Username = x.Username
                }).ToList();
        }

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
            _unitOfWork.SaveChanges();

            return user.Id;
        }

        public void RegisterUser(String username, String password, String email)
        {
            Register(username, password, email, Role.User);
        }

        public void RegisterOwner(String username, String password, String email)
        {
            Register(username, password, email, Role.Administrator);
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
            return _unitOfWork.AccountRepository.Read().FirstOrDefault(x => x.Id == id);
        }

        protected Account GetUser(String username)
        {
            return _unitOfWork.AccountRepository.Read().FirstOrDefault(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }

        protected Account GetUser(String username, String password)
        {
            return _unitOfWork.AccountRepository.Read().FirstOrDefault(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                x.Password.Equals(password, StringComparison.InvariantCultureIgnoreCase));
        }

        private void Register(String username, String password, String email, Role role)
        {
            var user = GetUser(username);
            if (user != null)
            {
                throw new UsernameAlreadyInUseException();
            }

            var userGroup = _unitOfWork.UserGroupRepository.Read().Where(x => x.Name.Equals("BaseGroupForUsers")).FirstOrDefault();

            user = new Account()
            {
                Username = username,
                Password = password,
                Remember = true,
                Role = role,
                UserGroup = userGroup,
                Profile = new Profile()
                {
                    Email = email,
                    Avatar = null
                }
            };

            _unitOfWork.AccountRepository.Create(user);
            _unitOfWork.SaveChanges();
        }

        #endregion
    }
}
