using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zetbox.Client.ASPNET;
using Zetbox.Client.Presentables;

namespace Zetbox.HR.ASPNET.Controllers
{
    public class HomeController : ZetboxController
    {
        public HomeController(IViewModelFactory vmf, ZetboxContextHttpScope contextScope)
            : base(vmf, contextScope)
        {
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
