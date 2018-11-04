using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WWImporters.Models;

namespace WWImporters.Controllers
{
    public class HomeController : Controller
    {
        private WWIContext db = new WWIContext();

        [HttpGet]
        public ActionResult Index(string query)
        {
            IEnumerable<Person> people = db.People.Where(p => p.FullName.Contains(null));
            ViewBag.ShowError = false;

            if(!string.IsNullOrWhiteSpace(query))
            {
                people = db.People.Where(p => p.FullName.Contains(query));
                if (!people.Any())
                {
                    ViewBag.ShowError = true;
                }
            }

            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
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