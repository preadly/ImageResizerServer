using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageResizeServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectPermanent("http://pread.ly");
        }

        public ActionResult HeartBeat()
        {
            return Content("OK");
        }

        public ActionResult Fala()
        {
            return Content("OK");
        }

    }
}