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
    public class UserManager : IUserManager
    {
        private IUnitOfWork _unitOfWork;

        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return _unitOfWork.AccountRepository.Read()
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    Username = x.Username,
                }).ToList();
        }

        public UserDTO GetUser(Int32 id)
        {
            return _unitOfWork.AccountRepository.Read()
                .Where(x => x.Id == id).Select(x => new UserDTO
                {
                    Id = x.Id,
                    Email = x.Profile.Email,
                    Username = x.Username
                }).FirstOrDefault();
        }

        public IEnumerable<UserFromGroupDTO> GetAllUsersCompareToSpecificGroup(int id)
        {
            List<UserFromGroupDTO> result = 
            _unitOfWork.AccountRepository.Read().Select(x => new UserFromGroupDTO()
            {
                Id = x.Id,
                InGroup = x.UserGroup.Id == id ? true : false, 
                Username = x.Username
            }).ToList();

            return result;
        }

        public void DeleteUser(Int32 id)
        {
            var user = _unitOfWork.AccountRepository.Read().FirstOrDefault(x => x.Id == id);
            _unitOfWork.AccountRepository.Delete(user);
            _unitOfWork.SaveChanges();
        }

        public Int32 SaveUser(UserDTO user)
        {
            Int32 id;
            if (user.Id == 0)
            {
                var profile = _unitOfWork.ProfileRepository.Create(new Profile()
                {
                    Email = user.Email,
                });

                var userGroup = _unitOfWork.UserGroupRepository.Read().Where(x => x.Name.Equals("BaseGroupForUsers")).FirstOrDefault();

                id = _unitOfWork.AccountRepository.Create(new Account()
                {
                    Password = user.Password,
                    Remember = false,
                    Username = user.Username,
                    Role = Role.User,
                    UserGroup = userGroup,
                    Profile = profile
                }).Id;
            }
            else
            {
                var us = _unitOfWork.AccountRepository.Read().Where(x => x.Id == user.Id).FirstOrDefault();
                us.Password = user.Password;
                us.Username = user.Username;
                us.Profile.Email = user.Email;
                id = us.Id;

                _unitOfWork.AccountRepository.Update(us);
            }
            _unitOfWork.SaveChanges();

            return id;
        }
    }
}
