using iTEC.BL.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.Managers
{
    public interface IUserGroupManager
    {
        IEnumerable<UserGroupDTO> GetUserGroups();
        UserGroupDTO GetUserGroup(int id);
        IEnumerable<UserDTO> GetUsersFromGroup(int id);
        void AddUserToGroup(int userGroupId, int userId);
        void RemoveUserFromGroup(int userId);
        void DeleteUserGroup(int id);
        Int32 SaveUserGroup(UserGroupDTO userGroup);
    }
}
