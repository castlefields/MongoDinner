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
            Assert.AreEqual(1, membership.Count(new Document() { { "Username", "test" } }));            
        }

        [Test]
        public void DeleteAUser()
        {
            var username = "testd";
            MembershipCreateStatus status;
            provider.CreateUser(username, "pass", "test@test", "test?", "test!", true, Oid.NewOid(), out status);
            Assert.AreEqual(MembershipCreateStatus.Success, status);
            Assert.AreEqual(1, membership.Count(new Document() { { "Username", username } }), "User not created");
            provider.DeleteUser("testd", true);
            Assert.AreEqual(0, membership.Count(new Document() { { "Username", username } }), "User not deleted");
        }
        [TestFixtureSetUp]
        public void Setup(){
            try{
//                foreach(var o in Membership.Providers){
//                    MembershipProvider p = o as MembershipProvider;
//                    Console.WriteLine(p.Name);
//                }
                provider =  new MongoDBMembershipProvider();
                provider.Initialize("MongoDBMembershipProvider", new NameValueCollection(){{"applicationName", "providertests"}});

                mongo = provider.Mongo;
                membership = mongo["providertests"]["membership"];
            }catch(Exception e){
                Console.WriteLine(e.Message);
                throw e;
            }
            CleanUp();
        }
        
        public void CleanUp(){
            membership.Delete(new Document());
        }
    }
}
