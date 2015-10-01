using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripsieAppWeb.Models
{
    public class CreateTrip
    {
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        [Required]
        public string Destination { get; set; }

        public string Description { get; set; }

        [Required]
        public string MyPhone { get; set; }

        [Required]
        public string MyName { get; set; }

        public string ContactsJson { get; set; }

        //List<Contact> Contacts { get; set; }
         
    }
}