using System;
using System.IO;
using System.Web.Mvc;

namespace iTEC.WebApplication.Controllers
{
    public class FileController : Controller
    {
        public FileResult GetExcelFile(String file)
        {
            var path = Path.Combine(Server.MapPath("~/Content/Files"), file);
            return File(path, "application/vnd.ms-excel", file);
        }
    }
}