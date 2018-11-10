using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiphyApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string apiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GiphyKey"];
            return View();
        }
    }
}