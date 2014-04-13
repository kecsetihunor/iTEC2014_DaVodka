using iTEC.BL.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.Managers
{
    public interface IAccountManager
    {
        IEnumerable<AccountAvatarDTO> GetAccounts();
        AccountAvatarDTO GetUserStateInfo(Int32 id);
        Int32 Authenticate(String username, String password, Boolean remember);
        void RegisterUser(String username, String password, String email);
        void RegisterOwner(String username, String password, String email);
        Int32 GetAccountId(String username);
    }
}
