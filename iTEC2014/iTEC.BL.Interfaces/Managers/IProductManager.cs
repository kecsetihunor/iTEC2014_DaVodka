using iTEC.BL.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.Managers
{
    public interface IProductManager
    {
        IEnumerable<PricedProductDTO> GetProducts();
        PricedProductDTO GetProduct(int id);
        IEnumerable<ProductFromCategoryDTO> GetAllProductsCompareToSpecificCategory(int categoryId);
        void DeleteProduct(int id);
        Int32 SaveProduct(PricedProductDTO userGroup);
    }
}
