using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MongoDB.Serialization;
using MongoDB.Configuration;
using MongoDB.Connections;
using MongoDB;
using MongoDB.Linq;

namespace MongoDinner.Models {

    public class DinnerRepository : MongoDinner.Models.IDinnerRepository {

        //NerdDinnerDataContext db = new NerdDinnerDataContext();
        static MongoConfiguration config = (MongoConfiguration)System.Configuration.ConfigurationManager.GetSection("Mongo");
        //static Connection conn = ConnectionFactory.GetConnection(config.ConnectionString);
        static IMongoDatabase db = new MongoDatabase(config.ConnectionString);
        static IMongoCollection<Dinner> dinners = db.GetCollection<Dinner>("dinners");


        //
        // Query Methods

        public IQueryable<Dinner> FindAllDinners() {
      
                return dinners.Linq<Dinner>();           
        }

        public IQueryable<Dinner> FindUpcomingDinners() {
            return from dinner in FindAllDinners()
                   where dinner.EventDate > DateTime.Now
                   orderby dinner.EventDate
                   select dinner;
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude) {
            //var dinners = from dinner in FindUpcomingDinners()
            //              join i in db.NearestDinners(latitude, longitude) 
            //              on dinner.DinnerID equals i.DinnerID
            //              select dinner;

            //return dinners;
            throw new NotImplementedException();
        }

        public Dinner GetDinner(Oid id) {
            //return db.Dinners.SingleOrDefault(d => d.DinnerID == id);
            return dinners.FindOne<Dinner>(d => d.DinnerID == id);
        }

        //
        // Insert/Delete Methods

        public void Add(Dinner dinner) {
            //db.Dinners.InsertOnSubmit(dinner);
            dinners.Insert(dinner);
        }

        public void Delete(Dinner dinner) {
            //db.RSVPs.DeleteAllOnSubmit(dinner.RSVPs);
            //db.Dinners.DeleteOnSubmit(dinner);
            dinners.Delete(dinner);
        }

        //
        // Persistence 

        public void Save() {
            //db.SubmitChanges();
            throw new NotImplementedException();
        }
    }
}
