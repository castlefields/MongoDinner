using System;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using MongoDinner.Helpers;
using MongoDB.Attributes;

namespace MongoDinner.Models
{
    public class Location
    {
        public Location() { }

        public Location(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        [MongoAlias("long")]
        public double Longitude { get; set; }

        [MongoAlias("lat")]
        public double Latitude { get; set; }
    }
}
