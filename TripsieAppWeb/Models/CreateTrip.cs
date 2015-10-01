using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripsieAppWeb.Models
{
    public class CreateTrip
    {
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public string Destination { get; set; }

        public string Description { get; set; }

        public string MyPhone { get; set; }

        public string MyName { get; set; }

        List<Contact> Contacts { get; set; }
         
    }
}