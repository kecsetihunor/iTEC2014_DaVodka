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
    public class ProductManager : IProductManager
    {
        private IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PricedProductDTO> GetProducts()
        {
            return _unitOfWork.ProductRepository.Read()
                .Select(x => new PricedProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToList();
        }

        public IEnumerable<PricedProductDTO> GetProductsFromCategory(int id)
        {
            return _unitOfWork.ProductRepository.Read().Where(x => x.Category.Id == id)
                .Select(x => new PricedProductDTO()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                }).ToList();
        }

        public PricedProductDTO GetProduct(Int32 id)
        {
            return _unitOfWork.ProductRepository.Read()
                .Where(x => x.Id == id).Select(x => new PricedProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).FirstOrDefault();
        }

        public IEnumerable<ProductFromCategoryDTO> GetAllProductsCompareToSpecificCategory(int categoryId)
        {
            List<ProductFromCategoryDTO> result =
                _unitOfWork.ProductRepository.Read().Select(x => new ProductFromCategoryDTO()
                {
                    Id = x.Id,
                    InGroup = x.Category.Id == categoryId ? true : false,
                    Name = x.Name
                }).ToList();

            return result;
        }

        public void DeleteProduct(Int32 id)
        {
            var product = _unitOfWork.ProductRepository.Read().FirstOrDefault(x => x.Id == id);
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.SaveChanges();
        }

        public Int32 SaveProduct(PricedProductDTO product)
        {
            Int32 id;
            if (product.Id == 0)
            {
                id = _unitOfWork.ProductRepository.Create(new Product()
                {
                    Name = product.Name,
                    Price = product.Price
                }).Id;
            }
            else
            {
                var prod = _unitOfWork.ProductRepository.Read().Where(x => x.Id == product.Id).FirstOrDefault();
                prod.Name = product.Name;
                prod.Price = product.Price;
                id = prod.Id;

                _unitOfWork.ProductRepository.Update(prod);
            }
            _unitOfWork.SaveChanges();
            return id;
        }
    }
}
