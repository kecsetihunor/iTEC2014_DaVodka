using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public class VoteRepository : GenericRepository<Vote>
    {
        public VoteRepository(ITECContext context)
            : base(context)
        {
        }

        public override DbSet<Vote> EntitySet
        {
            get
            {
                return context.Votes;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
    }
}
