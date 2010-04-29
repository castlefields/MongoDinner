using System;
using System.Linq;
using MongoDB;

namespace MongoDinner.Models {

    public interface IDinnerRepository {

        IQueryable<Dinner> FindAllDinners();
        IQueryable<Dinner> FindByLocation(float latitude, float longitude);
        IQueryable<Dinner> FindUpcomingDinners();
        Dinner GetDinner(Oid id);

        void Add(Dinner dinner);
        void Delete(Dinner dinner);
        
        void Save();
    }
}
