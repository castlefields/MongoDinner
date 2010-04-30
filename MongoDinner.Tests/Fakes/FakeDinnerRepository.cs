using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDinner.Models;
using MongoDB;

namespace MongoDinner.Tests.Fakes {

    public class FakeDinnerRepository : IDinnerRepository {

        private List<Dinner> dinnerList;

        public FakeDinnerRepository(List<Dinner> dinners) {
            dinnerList = dinners;
        }

        public IQueryable<Dinner> FindAllDinners() {
            return dinnerList.AsQueryable();
        }

        public IQueryable<Dinner> FindUpcomingDinners() {
            return (from dinner in dinnerList
                    where dinner.EventDate > DateTime.Now
                    select dinner).AsQueryable();
        }

        public IQueryable<Dinner> FindByLocation(float lat, float lon) {
            return (from dinner in dinnerList
                    where dinner.Location.Latitude == lat && dinner.Location.Longitude == lon
                    select dinner).AsQueryable();
        }

        public Dinner GetDinner(Oid id) {
            return dinnerList.SingleOrDefault(d => d.DinnerID == id);
        }

        public void Add(Dinner dinner) {
            dinnerList.Add(dinner);
        }

        public void Delete(Dinner dinner) {
            dinnerList.Remove(dinner);
        }

        public void Save() {
            foreach (Dinner dinner in dinnerList) {
                if (!dinner.IsValid)
                    throw new ApplicationException("Rule violations");
            }
        }
    }
}
