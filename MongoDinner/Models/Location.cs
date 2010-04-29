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

        public Location(float longitude, float latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        [MongoAlias("long")]
        public float Longitude { get; set; }

        [MongoAlias("lat")]
        public float Latitude { get; set; }
    }
}
