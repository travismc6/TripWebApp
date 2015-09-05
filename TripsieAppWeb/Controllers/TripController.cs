using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TripsieAppWeb.Models;

namespace TripsieAppWeb.Controllers
{
    public class TripController : Controller
    {
        public ActionResult Detail(string id)
        {
            TripAppEntities entities = new TripAppEntities();

            var code = entities.TripUsers.Where(r => r.TripCode == id).FirstOrDefault();

            if (code != null)
            {
                ViewBag.TripCode = id;
                return View("TripDetail", code);
            }

            else
            {
                ViewBag.IsFound = false;

                return RedirectToAction("../Home/EnterCode");
            }
        }

        public ActionResult Activities(string id)
        {
            TripAppEntities entities = new TripAppEntities();

            ViewBag.TripCode = id;
            return View(entities.TripUsers.First(r => r.TripCode == id));
        }

        public ActionResult UploadPicture(string id)
        {
            ViewBag.UserId = id;
            ViewBag.TripCode = id;

            return View(new UploadImageFile { TripUserId = id });
        }

        public ActionResult Pictures(string id)
        {
            TripAppEntities entities = new TripAppEntities();

            var trip = entities.TripUsers.Where(r => r.TripCode == id).FirstOrDefault().Trip;

            var pictures = entities.TripPictures.Where(r => r.TripUser.TripId == trip.Id).ToList();

            ViewBag.UserId = id;

            ViewBag.TripCode = id;

            return View(pictures);
        }

        public ActionResult Prizes(string id)
        {
            TripAppEntities entities = new TripAppEntities();

            var trip = entities.TripUsers.Where(r => r.TripCode == id).FirstOrDefault().Trip;

            ViewBag.TripCode = id;
            return View();
        }

        public ActionResult ActivityDetails(int id, string code)
        {
            TripAppEntities entities = new TripAppEntities();

            var activity = entities.TripActivities.FirstOrDefault(r=> r.Id == id);

            ViewBag.Comments = entities.TripComments.Where(r => r.TripActivityId != null && r.TripActivityId == id).OrderByDescending(r=> r.Date).ToList();
            ViewBag.createdBy = entities.TripUsers.FirstOrDefault(r=> r.Id == activity.TripUserId);
            ViewBag.TripUser = entities.TripUsers.FirstOrDefault(r=> r.TripCode == code);
            ViewBag.TripCode = id;
            return View(activity);
        }

        public ActionResult Comments(string id)
        {
            TripAppEntities entities = new TripAppEntities();

            var tripUser = entities.TripUsers.Where(r=> r.TripCode == id).First();

            ViewBag.Destination = tripUser.Trip.Destination;
            ViewBag.TripUser = tripUser;
            ViewBag.TripCode = id;
            return View("Comments", entities.TripComments.Where(r=> r.TripUser.TripId == tripUser.TripId && r.TripActivityId == 0).OrderByDescending(r=> r.Date).ToList());
        }

        [HttpPost]
        public ActionResult UploadPicture(UploadImageFile image)
        {
            var f = Request.Files[0];

            bool success = false;
            string urlSuffix = image.TripUserId + DateTime.Now.ToString() + ".jpg" ;
            string bucket = "trip-app-pictures";
            double lat = 0;
            double lon = 0;

            try
            {
                IAmazonS3 client = new AmazonS3Client("AKIAJWFD6TJO7NXPGCOA", "0MiV/p3r411kt9L5PE/Lwwg1n/2VLzbwXgRXIZLk", Amazon.RegionEndpoint.USEast1);

                // get lat lon
                if (!String.IsNullOrEmpty(image.Location))
                {

                    if (image.UseCurrentLocation)
                    {
                        var latlon = image.Location.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        lat = Convert.ToDouble(latlon[0]);
                        lon = Convert.ToDouble(latlon[1]);
                    }

                    else if (!String.IsNullOrEmpty(image.Location))
                    {
                        var address = image.Location;

                        var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

                        var georequest = WebRequest.Create(requestUri);
                        var response = georequest.GetResponse();
                        var xdoc = XDocument.Load(response.GetResponseStream());

                        var result = xdoc.Element("GeocodeResponse").Element("result");
                        var locationElement = result.Element("geometry").Element("location");
                        lat = Convert.ToDouble( locationElement.Element("lat").Value );
                        lon = Convert.ToDouble( locationElement.Element("lng").Value );
                    }
                }

                if (!String.IsNullOrEmpty(image.Description) && !String.IsNullOrEmpty(image.TripUserId) && f.InputStream != null)
                {
                    var request = new PutObjectRequest()
                    {
                        BucketName = bucket,
                        CannedACL = S3CannedACL.PublicRead,
                        Key = string.Format(urlSuffix),
                        InputStream = f.InputStream
                    };

                    client.PutObject(request);

                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;

            }
            
            if (success)
            {
                TripAppEntities entities = new TripAppEntities();
                var user = entities.TripUsers.Where(r => r.TripCode == image.TripUserId).FirstOrDefault();


                var url = "https://s3.amazonaws.com/trip-app-pictures/" + urlSuffix;

                TripPicture picture = new TripPicture();
                picture.TripUserId = user.Id;
                picture.Description = image.Description;
                picture.Date = DateTime.Now;
                picture.PictureUrl = url;
                picture.LocationName = !image.UseCurrentLocation ? image.Location : null;
                picture.Lat = lat;
                picture.Lon = lon;

                entities.TripPictures.Add(picture);
                entities.SaveChanges();

            }

            return RedirectToAction("../Trip/Pictures/" + image.TripUserId);
        }

        public ActionResult PicturesMap(string id)
        {
            ViewBag.TripCode = id;
            return View();
        }

        //public ActionResult TripDetail(string id)
        //{
        //    TripAppEntities entities = new TripAppEntities();

        //    var code = entities.TripUsers.Where(r => r.TripCode == id).FirstOrDefault();

        //    if (code != null)
        //    {
        //        return View(code);
        //    }

        //    else
        //    {
        //        ViewBag.IsFound = false;

        //        return RedirectToAction("../Home/EnterCode");
        //    }
        //}
    }
}