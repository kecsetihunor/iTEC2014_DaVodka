using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public class ProfileRepository : GenericRepository<Profile>
    {
        public ProfileRepository(ITECContext context)
            : base(context)
        {
        }

        public override DbSet<Profile> EntitySet
        {
            get
            {
                return context.Profiles;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
    }
}
