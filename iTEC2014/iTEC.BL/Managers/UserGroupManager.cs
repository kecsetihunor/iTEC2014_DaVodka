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
    public class UserGroupManager : IUserGroupManager
    {
        private IUnitOfWork _unitOfWork;

        public UserGroupManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserGroupDTO> GetUserGroups()
        {
            List<UserGroupDTO> result = new List<UserGroupDTO>();
            foreach (var userGroup in _unitOfWork.UserGroupRepository.Read())
            {
                if (!userGroup.Name.Equals("BaseGroupForUsers"))
                {
                    result.Add(new UserGroupDTO
                    {
                        Id = userGroup.Id,
                        Name = userGroup.Name,
                        Points = userGroup.Points
                    });
                }
            }

            return result.AsEnumerable();
        }

        public UserGroupDTO GetUserGroup(Int32 id)
        {
            return _unitOfWork.UserGroupRepository.Read()
                .Where(x => x.Id == id).Select(x => new UserGroupDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Accounts.Count,
                    Points = x.Points
                }).FirstOrDefault();
        }

        public IEnumerable<UserDTO> GetUsersFromGroup(int id)
        {
            return _unitOfWork.AccountRepository.Read().Where(x => x.UserGroup.Id == id)
                .Select(x => new UserDTO()
                {
                    Username = x.Username,
                    Email = x.Profile.Email,
                    Id = x.Id
                }).ToList();
        }

        public void AddUserToGroup(int userGroupId, int userId)
        {
            var user = _unitOfWork.AccountRepository.Read().Where(x => x.Id == userId).FirstOrDefault();
            var userGroup = _unitOfWork.UserGroupRepository.Read().Where(x => x.Id == userGroupId).FirstOrDefault();
            user.UserGroup = userGroup;

            _unitOfWork.AccountRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public void RemoveUserFromGroup(int userId)
        {
            var user = _unitOfWork.AccountRepository.Read().Where(x => x.Id == userId).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var userGroup = _unitOfWork.UserGroupRepository.Read().FirstOrDefault(x => x.Name.Equals("BaseGroupForUsers"));

            user.UserGroup = userGroup;

            _unitOfWork.AccountRepository.Update(user);
            _unitOfWork.SaveChanges();
        }

        public void DeleteUserGroup(Int32 id)
        {
            var userGroup = _unitOfWork.UserGroupRepository.Read().FirstOrDefault(x => x.Id == id);
            _unitOfWork.UserGroupRepository.Delete(userGroup);
            _unitOfWork.SaveChanges();
        }

        public Int32 SaveUserGroup(UserGroupDTO userGroup)
        {
            Int32 id;
            if (userGroup.Id == 0)
            {
                id = _unitOfWork.UserGroupRepository.Create(new UserGroup()
                {
                    Name = userGroup.Name,
                    Points = userGroup.Points
                }).Id;
            }
            else
            {
                var ug = _unitOfWork.UserGroupRepository.Read().Where(x => x.Id == userGroup.Id).FirstOrDefault();
                ug.Name = userGroup.Name;
                ug.Points = userGroup.Points;
                id = ug.Id;

                _unitOfWork.UserGroupRepository.Update(ug);
            }
            _unitOfWork.SaveChanges();
            return id;
        }
    }
}
