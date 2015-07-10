using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace LicenseManager.Api.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            Logger.Debug("HomeController called");
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
