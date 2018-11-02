using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WWImporters.Models;

namespace WWImporters.Controllers
{
    public class HomeController : Controller
    {
        private WWIContext db = new WWIContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(string query)
        {
            IEnumerable<Person> people = db.People.Where(p => p.FullName.Contains(query));
            if(people != null)
            {
                Debug.WriteLine("I found people");
                Debug.WriteLine(DateTime.Now);
                return View(people);
            }
            else
            {
                ViewBag.Message = "No results found.";
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}