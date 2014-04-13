using iTEC.Services.AccessLayer;
using iTEC.Services.AccessLayer.DelightService;
using iTEC.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iTEC.WebApplication.Controllers
{
    public class ProductController : SafeApiController
    {
        [HttpPost]
        public HttpResponseMessage GetProducts()
        {
            return SafeAction(() =>
           {
               IDelightServices service = new DelightServices();
               var products = service.GetProducts();
               if (products == null)
               {
                   return Request.CreateResponse(HttpStatusCode.NoContent);
               }
               var response = Request.CreateResponse<IEnumerable<ProductDTO>>(HttpStatusCode.OK, products);
               return response;
           });
        }

        [HttpPost]
        public HttpResponseMessage GetProduct([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
           {
               IDelightServices service = new DelightServices();
               var product = service.GetProduct(model.Id);
               if (product == null)
               {
                   return Request.CreateResponse(HttpStatusCode.NoContent);
               }
               var response = Request.CreateResponse<ProductDTO>(HttpStatusCode.OK, product);
               return response;
           }, model);
        }

        [HttpPost]
        public HttpResponseMessage GetAllProductsCompareToSpecificCategory([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
           {
               IDelightServices service = new DelightServices();
               var products = service.GetAllProductsCompareToSpecificCategory(model.Id)
                   .OrderByDescending(x => x.InGroup); ;
               if (products == null)
               {
                   return Request.CreateResponse(HttpStatusCode.NoContent);
               }
               var response = Request.CreateResponse<IEnumerable<ProductFromCategoryDTO>>(HttpStatusCode.OK, products);
               return response;
           }, model);
        }

        [HttpPost]
        public HttpResponseMessage DeleteProduct([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
           {
               IDelightServices service = new DelightServices();
               service.DeleteProduct(model.Id);
               var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
               return response;
           }, model);
        }

        [HttpPost]
        public HttpResponseMessage SaveProduct([FromBody]PricedProductDTO product)
        {
            return SafeAction(() =>
           {
               IDelightServices service = new DelightServices();
               int id = service.SaveProduct(product);
               if (id <= 0)
               {
                   return Request.CreateResponse(HttpStatusCode.NoContent);
               }
               var response = Request.CreateResponse<int>(HttpStatusCode.OK, id);
               return response;
           }, product);
        }
    }
}
