using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        [HttpGet]
        public ActionResult MileConverter()
        {
            double miles;
            double metricResult = 0; //where metric conversion values will be stored
            string unit = Request.QueryString["unitselect"];
            ViewBag.ValidUnit = false;

            if(Request.QueryString["btn"] == "Convert") //has "Convert" button been pressed?
            {
                if (Double.TryParse(Request.QueryString["mileinput"], out miles)) //can input value be converted to a double?
                {
                    if(miles >= 0) //input value a positive number?
                    {
                        if (unit == "millimeters")
                        {
                            metricResult = miles * 1609000;
                            ViewBag.ValidUnit = true;
                        }
                        else if (unit == "centimeters")
                        {
                            metricResult = miles * 160934.4;
                            ViewBag.ValidUnit = true;
                        }
                        else if (unit == "meters")
                        {
                            metricResult = miles * 1609.344;
                            ViewBag.ValidUnit = true;
                        }
                        else if (unit == "kilometers")
                        {
                            metricResult = miles * 1.609;
                            ViewBag.ValidUnit = true;
                        }
                        else //not a valid unit of measure
                        {
                            ViewBag.Display = "Invalid unit type. Please try again.";
                        }
                    }
                    else
                    {
                        ViewBag.Display = "Negative values are not allowed. Please try again.";
                    }
                    

                    if (ViewBag.ValidUnit)
                    {
                        ViewBag.Display = miles + " miles is equal to " + metricResult + " " + unit;
                    }
                }
                else
                {
                    ViewBag.Display = "Invalid input. Please try again.";
                }
            }

            return View();
        }
    }
}