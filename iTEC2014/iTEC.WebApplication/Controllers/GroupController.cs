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
    public class GroupController : SafeApiController
    {
        [HttpPost]
        public HttpResponseMessage GetGroups()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var userGroups = service.GetUserGroups();
                if (userGroups == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<IEnumerable<UserGroupDTO>>(HttpStatusCode.OK, userGroups);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetGroup([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var userGroup = service.GetUserGroup(model.Id);
                if (userGroup == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<UserGroupDTO>(HttpStatusCode.OK, userGroup);
                return response;
            }, model);
        }

        [HttpPost]
        public HttpResponseMessage GetUsersFromGroup([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var userGroups = service.GetUsersFromGroup(model.Id);
                if (userGroups == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<IEnumerable<UserDTO>>(HttpStatusCode.OK, userGroups);
                return response;
            }, model);
        }


        [HttpPost]
        public HttpResponseMessage AddUserToGroup([FromBody]GroupDTO model)
        {
            return SafeAction(() =>
           {
               IDelightServices service = new DelightServices();
               service.AddUserToGroup(model.GroupId, model.EntityId);
               var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
               return response;
           }, model);
        }

        [HttpPost]
        public HttpResponseMessage RemoveUserFromGroup([FromBody]IdentityDTO model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                service.RemoveUserFromGroup(model.Id);
                var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
                return response;
            }, model);
        }

        [HttpPost]
        public HttpResponseMessage DeleteGroup([FromBody]IdentityModel model)
        {
            return SafeAction(() =>
           {
               IDelightServices service = new DelightServices();
               service.DeleteUserGroup(model.Id);
               var response = Request.CreateResponse<bool>(HttpStatusCode.OK, true);
               return response;
           }, model);
        }

        [HttpPost]
        public HttpResponseMessage SaveGroup(UserGroupDTO userGroup)
        {
            return SafeAction(() =>
           {
               IDelightServices service = new DelightServices();
               int id = service.SaveUserGroup(userGroup);
               if (id <= 0)
               {
                   return Request.CreateResponse(HttpStatusCode.NoContent);
               }
               var response = Request.CreateResponse<int>(HttpStatusCode.OK, id);
               return response;
           }, userGroup);
        }
    }
}
