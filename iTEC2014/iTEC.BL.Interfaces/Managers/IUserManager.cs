using iTEC.BL.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.Managers
{
    public interface IUserManager
    {
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUser(Int32 id);
        IEnumerable<UserFromGroupDTO> GetAllUsersCompareToSpecificGroup(int id);
        void DeleteUser(Int32 id);
        Int32 SaveUser(UserDTO user);
    }
}
