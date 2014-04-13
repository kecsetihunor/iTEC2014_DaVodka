using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public class MessageRepository : GenericRepository<Message>
    {
        public MessageRepository(ITECContext context)
            : base(context)
        {
        }

        public override DbSet<Message> EntitySet
        {
            get
            {
                return context.Messages;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
    }
}
