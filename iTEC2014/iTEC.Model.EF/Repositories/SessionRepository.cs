using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public class SessionRepository : GenericRepository<Session>
    {
        public SessionRepository(ITECContext context)
            : base(context)
        {
        }

        public override DbSet<Session> EntitySet
        {
            get
            {
                return context.Sessions;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
    }
}
