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

        [HttpPost]
        public ViewResult InviteFriendsAction(Trip trip)
        {
            return View("TripDetails");
        }

        [HttpPost]
        public ViewResult CreateTripAction(CreateTrip createTrip)
        {
            Trip trip = new Trip
            {
                Description = createTrip.Description,
                Destination = createTrip.Destination,
                StartDate = createTrip.Begin.Date.ToString(),
                EndDate = createTrip.End.Date.ToString(),
                MyName = createTrip.MyName
            };

            trip.TripUsers = new List<TripUser>();

            TripUser userMe = new TripUser
            {
                DisplayName = createTrip.MyName,
                Phone = createTrip.MyPhone
            };

            trip.TripUsers.Add(userMe);


            return View("InviteFriends", trip);
        }

        public ViewResult InviteFriends(Trip trip)
        {
            return View(trip);
        }
    }
}