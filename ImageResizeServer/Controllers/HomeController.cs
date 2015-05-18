using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageResizeServer.Models;

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

        public ActionResult Browse(string pathInfo)
        {
            var url = "http://pread.ly/" + pathInfo;
            var browser = new WebBrowserJS(url, 1024, 1080);
            browser.GenerateWebSite();
            return Content(browser.innerHtml);
        }

    }
}