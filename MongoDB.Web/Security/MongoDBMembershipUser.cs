using System;
using System.Web.Security;

using MongoDB.Attributes;

namespace MongoDB.Web.Security
{
    
    public class MongoDBMembershipUser
    {
        [MongoAlias("_id")]
        public object ProviderUserKey{get; set;}
        public string Password{get; set;}
        public string PasswordAnswer{get; set;}
        public string ProviderName{get; set;}
        public string Name{get; set;}
        public string Username{get; set;}
        public string Email{get; set;}
        public string PasswordQuestion{get; set;}
        public string Comment{get; set;}
        public bool IsApproved{get; set;}
        public bool IsLockedOut{get; set;}
        public DateTime CreationDate{get; set;}
        public DateTime LastLoginDate{get; set;}
        public DateTime LastActivityDate{get; set;}
        public DateTime LastPasswordChangedDate{get; set;}
        public DateTime LastLockedOutDate{get; set;}
        
        public MongoDBMembershipUser(){}
        
        public static explicit operator MembershipUser(MongoDBMembershipUser mu){
            return new MembershipUser(mu.ProviderName, mu.Name, mu.ProviderUserKey,mu.Email
                                      ,mu.PasswordQuestion, mu.Comment, mu.IsApproved
                                      ,mu.IsLockedOut, mu.CreationDate, mu.LastLoginDate
                                      ,mu.LastActivityDate, mu.LastPasswordChangedDate
                                      ,mu.LastLockedOutDate);
        }
    }
}
