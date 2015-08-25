using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripsieAppWeb.Models;

namespace TripsieAppWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ViewResult EnterCode()
        {
            return View();
        }

        public ViewResult ForgotCode()
        {
            return View();
        }

        public ViewResult CreateTrip()
        {
            return View();
        }
    }
}