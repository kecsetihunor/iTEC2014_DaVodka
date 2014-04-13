using iTEC.Captcha;
using iTEC.Providers;
using iTEC.WebApplication.Captcha.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iTEC.WebApplication.Controllers
{
    public class CaptchaController : SafeApiController
    {
        [HttpPost]
        public HttpResponseMessage Create([FromBody]CaptchaSizeModel model)
        {
            return SafeAction(() =>
            {
                var captcha = CaptchaProvider.CreateCaptcha(model.Width, model.Height);

                return Request.CreateResponse(HttpStatusCode.OK, new CaptchaImageModel()
                {
                    Id = captcha.Id,
                    Image = DataUriSchemeProvider.CreatePNGImage(captcha.Image)
                });
            });
        }
    }
}
