using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public class AccountRepository : GenericRepository<Account>
    {
        public AccountRepository(ITECContext context)
            : base(context)
        {
        }

        public override DbSet<Account> EntitySet
        {
            get
            {
                return context.Accounts;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
    }
}
