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
    [Authorize]
    public class CategoryController : SafeApiController
    {
        [HttpPost]
        public HttpResponseMessage GetCategories()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var categories = service.GetCategories();
                if (categories == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<IEnumerable<CategoryDTO>>(HttpStatusCode.OK, categories);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetVotedCategories()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var categories = service.GetVotedCategories(User.Identity.Name);
                if (categories == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<IEnumerable<VotedCategoryDTO>>(HttpStatusCode.OK, categories);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetCategory([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
          {
              IDelightServices service = new DelightServices();
              var category = service.GetCategory(model.Id);
              if (category == null)
              {
                  return Request.CreateResponse(HttpStatusCode.NoContent);
              }
              var response = Request.CreateResponse<CategoryDTO>(HttpStatusCode.OK, category);
              return response;
          }, model);
        }

        [HttpPost]
        public HttpResponseMessage GetProductsFromCategory([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var products = service.GetProductsFromCategory(User.Identity.Name, model.Id);
                if (products == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<IEnumerable<VotedProductDTO>>(HttpStatusCode.OK, products);
                return response;
            }, model);
        }

        [HttpPost]
        public HttpResponseMessage AddProductToCategory([FromBody]GroupDTO model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                service.AddProductToCategory(model.GroupId, model.EntityId);
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
                return response;
            }, model);
        }

        [HttpPost]
        public HttpResponseMessage RemoveProductFromCategory([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                service.RemoveProductFromCategory(model.Id);
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
                return response;
            }, model);
        }

        [HttpPost]
        public HttpResponseMessage DeleteCategory([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
          {
              IDelightServices service = new DelightServices();
              service.DeleteCategory(model.Id);
              var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
              return response;
          }, model);
        }

        [HttpPost]
        public HttpResponseMessage SaveCategory([FromBody]CategoryDTO category)
        {
            return SafeAction(() =>
          {
              IDelightServices service = new DelightServices();
              int id = service.SaveCategory(category);
              if (id <= 0)
              {
                  return Request.CreateResponse(HttpStatusCode.NoContent);
              }
              var response = Request.CreateResponse<int>(HttpStatusCode.OK, id);
              return response;
          }, category);
        }
    }
}
