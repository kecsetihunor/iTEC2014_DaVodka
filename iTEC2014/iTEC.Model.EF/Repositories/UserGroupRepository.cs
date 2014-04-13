using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public class UserGroupRepository : GenericRepository<UserGroup>
    {
        public UserGroupRepository(ITECContext context)
            : base(context)
        {
        }

        public override DbSet<UserGroup> EntitySet
        {
            get
            {
                return context.UserGroups;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
    }
}
