using iTEC.BL.Interfaces.DTOs;
using iTEC.BL.Interfaces.Managers;
using iTEC.ErrorHandling.Exceptions;
using iTEC.Model.Entities;
using iTEC.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.BL.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            List<CategoryDTO> result = new List<CategoryDTO>();
            foreach (var category in _unitOfWork.CategoryRepository.Read())
            {
                if (!category.Name.Equals("BaseCategoryForProducts"))
                {
                    result.Add(new CategoryDTO
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Count = category.Products.Count
                    });
                }
            }

            return result.AsEnumerable();
        }

        public CategoryDTO GetCategory(Int32 id)
        {
            return _unitOfWork.CategoryRepository.Read()
                .Where(x => x.Id == id).Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Products.Count
                }).FirstOrDefault();
        }

        public IEnumerable<VotedProductDTO> GetProductsFromCategory(int id)
        {
            return _unitOfWork.ProductRepository.Read().Where(x => x.Category.Id == id)
                .Select(x => new VotedProductDTO()
                {
                    Name = x.Name,
                    Price = x.Price,
                    Id = x.Id
                }).ToList().AsEnumerable();
        }

        public void AddProductToCategory(int categoryId, int productId)
        {
            var product = _unitOfWork.ProductRepository.Read().Where(x => x.Id == productId).FirstOrDefault();
            var category = _unitOfWork.CategoryRepository.Read().Where(x => x.Id == categoryId).FirstOrDefault();
            
            product.Category = category; ;

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.SaveChanges();
        }

        public void RemoveProductFromCategory(int productId)
        {
            var product = _unitOfWork.ProductRepository.Read().Where(x => x.Id == productId).FirstOrDefault();
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var category = _unitOfWork.CategoryRepository.Read().FirstOrDefault(x => x.Name.Equals("BaseCategoryForProducts"));

            product.Category = category;

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.SaveChanges();
        }

        public void DeleteCategory(Int32 id)
        {
            var category = _unitOfWork.CategoryRepository.Read().FirstOrDefault(x => x.Id == id);
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.SaveChanges();
        }

        public Int32 SaveCategory(CategoryDTO category)
        {
            Int32 id;
            if (category.Id == 0)
            {
                id = _unitOfWork.CategoryRepository.Create(new Category()
                {
                    Name = category.Name,
                    Products = new List<Product>()
                }).Id;
            }
            else
            {
                var cat = _unitOfWork.CategoryRepository.Read().Where(x => x.Id == category.Id).FirstOrDefault();
                cat.Name = category.Name;
                id = cat.Id;
                _unitOfWork.CategoryRepository.Update(cat);
            }
            _unitOfWork.SaveChanges();
            return id;
        }
    }
}
