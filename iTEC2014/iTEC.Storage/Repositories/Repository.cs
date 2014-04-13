using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace iTEC.Storage.Repositories
{
    public abstract class AbstractRepository<TContext> : IDisposable
        where TContext : DbContext, new()
    {
        #region Fields

        protected TContext context;

        #endregion

        #region Initialization

        public AbstractRepository()
        {
            context = new TContext();
        }

        public AbstractRepository(TContext ctx)
        {
            context = ctx;
        }

        #endregion

        #region Logic

        protected abstract IQueryable<K> GetAll<K>()
            where K : class;

        protected abstract K Add<K>(K entity)
            where K : class;

        protected abstract void Delete<K>(K entity)
            where K : class;

        protected abstract void Save();

        #endregion

        #region Dispose

        public void Dispose()
        {
            context.Dispose();
        }

        #endregion
    }

    public abstract class AbstractPagerRepository<TContext> : AbstractRepository<TContext>
        where TContext : DbContext, new()
    {
        protected abstract IQueryable<TSource> Page<TSource, TKey>(Expression<Func<TSource, Boolean>> where, Expression<Func<TSource, TKey>> orderBySelector, Boolean ascending, Pager pager = null)
            where TSource : class;

        protected abstract Pager Find<TSource, TKey>(Expression<Func<TSource, Boolean>> where, Expression<Func<TSource, TKey>> orderBySelector, Boolean ascending)
            where TSource : class;
    }
    
    public abstract class Repository<TContext> : AbstractRepository<TContext>
        where TContext : DbContext, new()
    {
        #region CRUD

        protected override IQueryable<K> GetAll<K>()
        {
            return context.Set<K>();
        }

        protected override K Add<K>(K entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            return context.Set<K>().Add(entity);
        }

        protected override void Delete<K>(K entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            context.Set<K>().Remove(entity);
        }

        protected override void Save()
        {
            context.SaveChanges();
        }

        #endregion
    }

    public abstract class PagerRepository<TContext> : Repository<TContext>
        where TContext : DbContext, new()
    {
        protected IQueryable<TSource> Page<TSource, TKey>(Expression<Func<TSource, Boolean>> where, Expression<Func<TSource, TKey>> orderBySelector, Boolean ascending, Pager pager = null)
            where TSource : class
        {
            var query = GetAll<TSource>().Where(where);
            query = (ascending)
                ? query.OrderBy(orderBySelector)
                : query.OrderByDescending(orderBySelector);

            if (pager != null && pager.Size > 0)
            {
                var count = GetAll<TSource>().Count(where);
                if (count > 0)
                {
                    pager.Total = (Int32)Math.Ceiling((Double)count / pager.Size);
                    if (pager.Current > pager.Total)
                    {
                        pager.Current = pager.Total;
                    }
                }
                else
                {
                    pager.Current = 1;
                    pager.Total = 1;
                }
                return query.Skip((pager.Current - 1) * pager.Size).Take(pager.Size);
            }
            else
            {
                return query;
            }
        }

        protected Pager Find<TSource, TKey>(Expression<Func<TSource, Boolean>> where, Predicate<TSource> counter, Expression<Func<TSource, TKey>> orderBySelector, Boolean ascending, Pager pager = null)
            where TSource : class
        {
            var query = GetAll<TSource>().Where(where);
            query = (ascending)
                ? query.OrderBy(orderBySelector)
                : query.OrderByDescending(orderBySelector);
            
            var list = query.ToList();
            var count = list.Count();
            if (count > 0)
            {
                if (pager == null)
                {
                    pager = new Pager();
                }
                if (pager.Size == 0)
                {
                    pager.Size = count;
                }
                pager.Total = (Int32)Math.Ceiling((Double)count / pager.Size);

                var index = list.FindIndex(counter) + 1;
                pager.Current = (Int32)Math.Ceiling((Double)index / pager.Size);
            }

            return pager;
        }
    }

    public class Pager
    {
        public Int32 Current { get; set; }
        public Int32 Size { get; set; }
        public Int32 Total { get; set; }
        public String Filter { get; set; }

        public Pager()
        {
            Size = 5;
        }
    }
}