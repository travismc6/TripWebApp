using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripsieAppWeb.Models
{
    public class UploadImageFile
    {
        public int TripId { get; set; }

        public string TripUserId { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public bool UseCurrentLocation { get; set; }
    }
}