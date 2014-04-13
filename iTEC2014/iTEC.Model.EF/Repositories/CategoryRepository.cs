using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(ITECContext context)
            : base(context)
        {
        }

        public override DbSet<Category> EntitySet
        {
            get
            {
                return context.Categories;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
    }
}
