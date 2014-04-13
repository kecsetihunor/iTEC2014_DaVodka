using iTEC.BL.Interfaces.Managers;
using iTEC.BL.Managers;
using iTEC.Model.EF;
using iTEC.Model.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTEC.Services
{
    public class IoCBootstrapper
    {
        private static bool _booted = false;

        public static void Boot()
        {
            if (!_booted)
            {
                ObjectFactory.Initialize(x =>
                {
                    x.For<IUnitOfWork>().Use<UnitOfWork>();
                    x.For<IAccountManager>().Use<AccountManager>();
                    x.For<ICategoryManager>().Use<CategoryManager>();
                    x.For<IProductManager>().Use<ProductManager>();
                    x.For<IUserGroupManager>().Use<UserGroupManager>();
                    x.For<IVoteManager>().Use<VoteManager>();
                    x.For<IUserManager>().Use<UserManager>();
                    x.For<ISessionManager>().Use<SessionManager>();
                    x.For<IMessageManager>().Use<MessageManager>();
                });
            }
        }
    }
}