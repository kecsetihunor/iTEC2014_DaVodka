using iTEC.BL.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.Managers
{
    public interface IMessageManager
    {
        MessageDTO GetMessage();
        void SaveMessage(MessageDTO message);
    }
}
