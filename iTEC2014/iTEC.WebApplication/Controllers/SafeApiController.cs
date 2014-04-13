using iTEC.ErrorHandling.Exceptions;
//using iTEC.Storage.Repositories.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iTEC.WebApplication.Controllers
{
    public class SafeApiController : ApiController
    {
        protected internal HttpResponseMessage SafeAction(Func<HttpResponseMessage> action)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return action();
                }
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError("", "[REPOSITORY] " + e.Message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "[GENERAL] " + e.Message);
            }

            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage));
            return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
        }

        protected internal HttpResponseMessage SafeAction(Func<HttpResponseMessage> action, Object model)
        {
            if (model != null)
            {
                return SafeAction(action);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}