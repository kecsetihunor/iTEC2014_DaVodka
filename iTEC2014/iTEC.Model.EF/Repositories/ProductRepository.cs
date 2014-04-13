using iTEC.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Model.EF.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ITECContext context)
            : base(context)
        {
        }

        public override DbSet<Product> EntitySet
        {
            get
            {
                return context.Products;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
    }
}
