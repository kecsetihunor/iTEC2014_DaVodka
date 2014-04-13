using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.Interfaces
{
    public interface IRepository<T>
    {
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
        T Read(int id);
        IQueryable<T> Read();
    }
}
