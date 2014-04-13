using iTEC.Captcha;
using iTEC.WebApplication.Common;
using iTEC.Providers;
using iTEC.WebApplication.User.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using iTEC.Services.AccessLayer;
using iTEC.ErrorHandling.Exceptions;
using iTEC.Services.AccessLayer.DelightService;

namespace iTEC.WebApplication.Controllers
{
    public class AccountController : SafeApiController
    {
        // GET api/account/state
        [HttpGet]
        public HttpResponseMessage State(Int32 id = 0)
        {
            return SafeAction(() =>
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetUserStateInfo(User.Identity.IsAuthenticated, User.Identity.Name, id));
            });
        }

        // POST api/account/authenticate
        [HttpPost]
        public HttpResponseMessage Authenticate([FromBody]AuthenticationAccountModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var id = service.Authenticate(model.Username, model.Password, model.Remember);
                CacheCookieProvider.CreateCookie(id).SetCookie();
                FormsAuthentication.SetAuthCookie(model.Username, model.Remember);

                return Request.CreateResponse(HttpStatusCode.OK, GetUserStateInfo(true, model.Username, id));
            }, model);
        }

        // POST api/account/logout
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Logout()
        {
            return SafeAction(() =>
            {
                FormsAuthentication.SignOut();
                //CacheCookieProvider.CreateExpiredCookie().SetCookie();

                return Request.CreateResponse(HttpStatusCode.OK, true);
            });
        }

        // POST api/account/register
        [HttpPost]
        public HttpResponseMessage Register([FromBody]RegistrationAccountModel model)
        {
            return SafeAction(() =>
            {
                if (!model.Password.Equals(model.Confirm, StringComparison.InvariantCulture))
                {
                    throw new InvalidPasswordsException();
                }
                if (!CaptchaProvider.Validate(model.Captcha.Id, model.Captcha.Text))
                {
                    throw new InvalidCaptchaCodeException();
                }

                IDelightServices service = new DelightServices();

                if (String.IsNullOrEmpty(model.Type))
                {
                    service.RegisterUser(model.Username, model.Password, model.Email);
                }
                else
                {
                    service.RegisterOwner(model.Username, model.Password, model.Email);
                }
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }, model);
        }

        #region Helpers

        [NonAction]
        private UserModel GetUserStateInfo(Boolean isAuthenticated, String username, Int32 id = 0)
        {
            var model = new UserModel()
            {
                IsAuthenticated = isAuthenticated,
                Username = username
            };

            if (id > 0)
            {
                try
                {
                    IDelightServices service = new DelightServices();

                    AccountAvatarDTO info = service.GetUserStateInfo(id);
                    model.Remember = info.Remember;
                    if (model.Remember)
                    {
                        model.Username = info.Username;
                        model.Avatar = DataUriSchemeProvider.CreatePNGImage(info.Avatar);
                    }
                    if (model.IsAuthenticated)
                    {
                        model.Role = info.Role;
                    }

                }
                catch (RepositoryException)
                {
                }
            }

            return model;
        }

        #endregion
    }
}
