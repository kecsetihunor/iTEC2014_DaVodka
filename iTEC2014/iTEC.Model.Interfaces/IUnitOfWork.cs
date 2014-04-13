using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Account> AccountRepository { get; }
        IRepository<Profile> ProfileRepository { get; }
        IRepository<UserGroup> UserGroupRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Vote> VoteRepository { get; }
        IRepository<Session> SessionRepository { get; }
        IRepository<Message> MessageRepository { get; }
        void SaveChanges();
    }
}
