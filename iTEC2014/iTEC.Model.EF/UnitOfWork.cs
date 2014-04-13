using iTEC.Model.EF.Repositories;
using iTEC.Model.Entities;
using iTEC.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ITECContext iTECContext;

        private IRepository<Account> accountRepository;
        private IRepository<Profile> profileRepository;
        private IRepository<UserGroup> userGroupRepository;
        private IRepository<Product> productRepository;
        private IRepository<Category> categoryRepository;
        private IRepository<Vote> voteRepository;
        private IRepository<Session> sessionRepository;
        private IRepository<Message> messageRepository;

        public UnitOfWork()
        {
            iTECContext = new ITECContext();
            Database.SetInitializer<ITECContext>(new iTECDbInitializer());
        }

        public IRepository<Account> AccountRepository
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new AccountRepository(iTECContext);
                }

                return accountRepository;
            }
        }

        public IRepository<Profile> ProfileRepository
        {
            get
            {
                if (profileRepository == null)
                {
                    profileRepository = new ProfileRepository(iTECContext);
                }

                return profileRepository;
            }
        }

        public IRepository<UserGroup> UserGroupRepository
        {
            get
            {
                if (userGroupRepository == null)
                {
                    userGroupRepository = new UserGroupRepository(iTECContext);
                }

                return userGroupRepository;
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(iTECContext);
                }

                return categoryRepository;
            }
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(iTECContext);
                }

                return productRepository;
            }
        }

        public IRepository<Vote> VoteRepository
        {
            get
            {
                if (voteRepository == null)
                {
                    voteRepository = new VoteRepository(iTECContext);
                }

                return voteRepository;
            }
        }

        public IRepository<Session> SessionRepository
        {
            get
            {
                if (sessionRepository == null)
                {
                    sessionRepository = new SessionRepository(iTECContext);
                }

                return sessionRepository;
            }
        }

        public IRepository<Message> MessageRepository
        {
            get
            {
                if (messageRepository == null)
                {
                    messageRepository = new MessageRepository(iTECContext);
                }

                return messageRepository;
            }
        }

        public void SaveChanges()
        {
            iTECContext.SaveChanges();
        }
    }
}
