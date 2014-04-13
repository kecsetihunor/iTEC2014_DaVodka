using iTEC.Captcha;
using iTEC.Storage.Repositories;
using iTEC.Storage.Repositories.Exceptions;
using iTEC.WebApplication.Common;
using iTEC.WebApplication.Providers;
using iTEC.WebApplication.User.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

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
                using (var repository = new AccountRepository())
                {
                    var id = repository.Authenticate(model.Username, model.Password, model.Remember);
                    CacheCookieProvider.CreateCookie(id).SetCookie();
                    FormsAuthentication.SetAuthCookie(model.Username, model.Remember);

                    return Request.CreateResponse(HttpStatusCode.OK, GetUserStateInfo(true, model.Username, id));
                }
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

                using (var repository = new AccountRepository())
                {
                    repository.Register(model.Username, model.Password, model.Email);
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
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
                    using (var repository = new AccountRepository())
                    {
                        var info = repository.GetUserStateInfo(id);
                        model.Remember = info.Remember;
                        if (model.Remember)
                        {
                            model.Username = info.Username;
                        }
                        model.Avatar = DataUriSchemeProvider.CreatePNGImage(info.Avatar);
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
