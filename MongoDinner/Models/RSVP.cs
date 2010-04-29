using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using MongoDinner.Helpers;
using MongoDB.Attributes;

namespace MongoDinner.Models
{
    public class RSVP
    {
        [MongoAlias("attendee_name")]
        public string AttendeeName { get; set; }
    }
}
