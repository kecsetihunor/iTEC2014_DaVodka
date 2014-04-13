using iTEC.Model.Entities;
using iTEC.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T>
           where T : AbstractIdentifiableEntity
    {
        protected ITECContext context;

        public GenericRepository(ITECContext context)
        {
            this.context = context;
        }

        public abstract DbSet<T> EntitySet { get; protected set; }

        public T Create(T entity)
        {
            return EntitySet.Add(entity);
        }

        public T Update(T entity)
        {
            var attachedValue = EntitySet.Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
            return attachedValue;
        }

        public void Delete(T entity)
        {
            EntitySet.Remove(entity);
        }

        public T Read(int id)
        {
            return EntitySet.FirstOrDefault<T>(x => x.Id == id);
        }

        public IQueryable<T> Read()
        {
            return EntitySet;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
