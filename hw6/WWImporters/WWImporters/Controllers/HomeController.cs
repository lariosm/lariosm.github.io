using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WWImporters.Models;
using WWImporters.Models.ViewModel;

namespace WWImporters.Controllers
{
    public class HomeController : Controller
    {
        private WWIContext db = new WWIContext();

        /// <summary>
        /// Performs the search based on the keywords passed from search box
        /// </summary>
        /// <param name="query">keyword(s) to search</param>
        /// <returns>A list of Person objects</returns>
        [HttpGet]
        public ActionResult Index(string query)
        {
            IEnumerable<Person> people = db.People.Where(p => p.FullName.Contains(null)); //start out with an empty container
            ViewBag.ShowError = false; //do not show search error if loading the page for the first time or after a successful search
            ViewBag.ResultString = ""; //shows empty "result" string if loading the page for the first time or after a failed search

            if(!string.IsNullOrWhiteSpace(query)) //makes sure query is not blank or contains only whitespaces.
            {
                people = db.People.Where(peopleItem => peopleItem.FullName.Contains(query)); //performs the query
                if (!people.Any()) //No matches? It's a failed search.
                {
                    ViewBag.ShowError = true; //display failed search message
                }
                else //successful search
                {
                    ViewBag.ResultString = "Names matching your search: \"" + query + "\"";
                }
            }

            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            DashboardVM vm = new DashboardVM();

            if (id == null) //Non-existant Person ID?
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //Send out a 400 bad request error.
            }
            vm.Person = db.People.Find(id);
            if (vm.Person == null) //Non-existant Person object?
            {
                return HttpNotFound(); //Send out a 404 not found error.
            }
            return View(vm);
        }
    }
}