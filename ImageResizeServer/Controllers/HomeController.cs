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
            return Content("OK3");
        }

        public ActionResult Fala()
        {
            return Content("OK3");
        }

        public ActionResult Browse(string pathInfo)
        {
            string url = "http://pread.ly/" + pathInfo;

            if (string.IsNullOrEmpty(pathInfo))
            {
                url = HttpUtility.UrlDecode(Request.QueryString.ToString());

                if (url.StartsWith("/"))
                    url = url.Substring(1);

                if (url.StartsWith("http"))
                {
                  var uri = new Uri(url);
                  url = "http://pread.ly" + uri.PathAndQuery;
                }
            }

            var browser = new WebBrowserJS(url, 1024, 1080);
            browser.GenerateWebSite();
            return Content(browser.innerHtml);
        }

    }
}