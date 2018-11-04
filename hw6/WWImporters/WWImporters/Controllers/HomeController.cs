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
            ViewBag.ResultString = "";

            if(!string.IsNullOrWhiteSpace(query))
            {
                people = db.People.Where(peopleItem => peopleItem.FullName.Contains(query));
                if (!people.Any())
                {
                    ViewBag.ShowError = true;
                }
                else
                {
                    ViewBag.ResultString = "Names matching your search: \"" + query + "\"";
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
    }
}