using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Security;

using NUnit.Framework;

using MongoDB;

namespace MongoDB.Web.Security
{
    [TestFixture]
    public class MongoDBMembershipProviderTests
    {
        MongoDBMembershipProvider provider;
        Mongo mongo;
        IMongoCollection membership;
        
        [Test]
        public void CreateAUser()
        {
            MembershipCreateStatus status;
            provider.CreateUser("test", "pass", "test@test", "test?", "test!", true, Oid.NewOid(), out status);
            Assert.AreEqual(MembershipCreateStatus.Success, status);
            
        }
        
        [TestFixtureSetUp]
        public void Setup(){
            provider = (MongoDBMembershipProvider)Membership.Provider;
            mongo = new Mongo();
            mongo.Connect();
            membership = mongo["providertests"]["membership"];
            
            CleanUp();
        }
        
        public void CleanUp(){
            membership.Delete(new Document());
        }
    }
}
