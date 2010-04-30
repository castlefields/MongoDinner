using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using MongoDinner.Helpers;
using MongoDB;
using MongoDB.Attributes;

namespace MongoDinner.Models {

    public class Dinner
    {
        [MongoAlias("_id")]
        public Oid DinnerID { get; set; }

        [MongoAlias("title")]
        public string Title {get; set;}

        [MongoAlias("event_date")]
        public DateTime EventDate { get; set; }

        [MongoAlias("description")]
        public string Description { get; set; }

        [MongoAlias("hosted_by")]
        public string HostedBy { get; set; }

        [MongoAlias("contact_phone")]
        public string ContactPhone { get; set; }

        [MongoAlias("address")]
        public string Address { get; set; }

        [MongoAlias("country")]
        public string Country { get; set; }

        [MongoAlias("loc")]
        public Location Location { get; set; }

        [MongoAlias("rsvp")]
        public List<RSVP> RSVPs { get; set; }

        public bool IsHostedBy(string userName)
        {
            return HostedBy.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations() {

            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("Title is required", "Title");

            if (String.IsNullOrEmpty(Description))
                yield return new RuleViolation("Description is required", "Description");

            if (String.IsNullOrEmpty(HostedBy))
                yield return new RuleViolation("HostedBy is required", "HostedBy");

            if (String.IsNullOrEmpty(Address))
                yield return new RuleViolation("Address is required", "Address");

            if (String.IsNullOrEmpty(Country))
                yield return new RuleViolation("Country is required", "Address");

            if (String.IsNullOrEmpty(ContactPhone))
                yield return new RuleViolation("Phone# is required", "ContactPhone");

            if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
                yield return new RuleViolation("Phone# does not match country", "ContactPhone");

            yield break;
        }

        public void OnValidate(ChangeAction action) {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
}
