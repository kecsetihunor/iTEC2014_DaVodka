using iTEC.Services.AccessLayer;
using System;
using System.Net.Http;
using System.Web.Mvc;

namespace iTEC.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(1236651);
        }
    }
}