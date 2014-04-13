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
    public class UserController : SafeApiController
    {
        [HttpPost]
        public HttpResponseMessage GetUsers()
        {
            return SafeAction(() =>
          {
              IDelightServices service = new DelightServices();
              var users = service.GetUsers();
              if (users == null)
              {
                  return Request.CreateResponse(HttpStatusCode.NoContent);
              }
              var response = Request.CreateResponse<IEnumerable<UserDTO>>(HttpStatusCode.OK, users);
              return response;
          });
        }

        [HttpPost]
        public HttpResponseMessage GetUser([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
          {
              IDelightServices service = new DelightServices();
              var user = service.GetUser(model.Id);
              if (user == null)
              {
                  return Request.CreateResponse(HttpStatusCode.NoContent);
              }
              var response = Request.CreateResponse<UserDTO>(HttpStatusCode.OK, user);
              return response;
          }, model);
        }

        [HttpPost]
        public HttpResponseMessage GetAllUsersCompareToSpecificGroup([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var user = service.GetAllUsersCompareToSpecificGroup(model.Id)
                    .OrderByDescending(x => x.InGroup);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<IEnumerable<UserFromGroupDTO>>(HttpStatusCode.OK, user);
                return response;
            }, model);
        }

        [HttpPost]
        public HttpResponseMessage DeleteUser([FromBody]IdentityModel user)
        {
            return SafeAction(() =>
          {
              IDelightServices service = new DelightServices();
              service.DeleteUser(user.Id);
              var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
              return response;
          }, user);
        }

        [HttpPost]
        public HttpResponseMessage SaveUser(UserDTO user)
        {
            return SafeAction(() =>
          {
              IDelightServices service = new DelightServices();
              int id = service.SaveUser(user);
              if (id <= 0)
              {
                  return Request.CreateResponse(HttpStatusCode.NoContent);
              }
              var response = Request.CreateResponse<int>(HttpStatusCode.OK, id);
              return response;
          }, user);
        }
    }
}
