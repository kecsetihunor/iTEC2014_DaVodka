using System.Web;

namespace iTEC.WebApplication.Common
{
    public static class CookieExtensions
    {
        public static void SetCookie(this HttpCookie cookie)
        {
            if (HttpContext.Current.Request.Cookies[cookie.Name] != null)
            {
                HttpContext.Current.Response.SetCookie(cookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}