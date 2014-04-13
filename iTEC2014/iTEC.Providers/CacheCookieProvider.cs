using System;
using System.Web;

namespace iTEC.Providers
{
    public static class CacheCookieProvider
    {
        public static String CookieName;

        static CacheCookieProvider()
        {
            CookieName = ".CACHE";
        }

        public static HttpCookie CreateCookie(Int32 id)
        {
            var cookie = new HttpCookie(CookieName, id.ToString());
            cookie.Expires = DateTime.Now.AddYears(10);
            return cookie;
        }

        public static HttpCookie CreateExpiredCookie()
        {
            var cookie = new HttpCookie(CookieName);
            cookie.Expires = new DateTime(1999, 1, 1);
            return cookie;
        }
    }
}