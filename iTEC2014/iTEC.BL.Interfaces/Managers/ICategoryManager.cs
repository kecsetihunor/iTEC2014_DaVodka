using iTEC.BL.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Interfaces.Managers
{
    public interface ICategoryManager
    {
        IEnumerable<CategoryDTO> GetCategories();
        CategoryDTO GetCategory(int id);
        IEnumerable<VotedProductDTO> GetProductsFromCategory(int id);
        void AddProductToCategory(int categoryId, int productId);
        void RemoveProductFromCategory(int productId);
        void DeleteCategory(int id);
        Int32 SaveCategory(CategoryDTO userGroup);
    }
}
