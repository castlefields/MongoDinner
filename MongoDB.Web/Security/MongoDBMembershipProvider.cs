using System;
using System.Web.Security;

using MongoDB;

namespace MongoDB.Web.Security
{
    public class MongoDBMembershipProvider: System.Web.Security.MembershipProvider
    {
        private Mongo mongo;
        private IMongoDatabase provider;
        private IMongoCollection<MongoDBMembershipUser> members;

        public override string Name {
            get { return "MongoDBMembershipProvider";}
        }
        public override string Description {
            get { return "Membership provider using MongoDB as a backend";}
        }
        
        public override string ApplicationName {get; set;}

        public override bool EnablePasswordReset {get{throw new NotImplementedException();}}
        public override bool EnablePasswordRetrieval {get{throw new NotImplementedException();}}
        public override int MaxInvalidPasswordAttempts {get{throw new NotImplementedException();} }
        public override int MinRequiredNonAlphanumericCharacters {get{throw new NotImplementedException();}}
        public override int MinRequiredPasswordLength {get{throw new NotImplementedException();}}
        public override int PasswordAttemptWindow {get{throw new NotImplementedException();} }
        public override MembershipPasswordFormat PasswordFormat {get{throw new NotImplementedException();} }
        public override string PasswordStrengthRegularExpression {get{throw new NotImplementedException();} }
        public override bool RequiresQuestionAndAnswer {get{throw new NotImplementedException();}}
        public override bool RequiresUniqueEmail {get{throw new NotImplementedException();}}

                
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            this.ApplicationName = GetConfigValue(config["applicationName"], "aspprovider");
            mongo = new Mongo();
            mongo.Connect();
            
            provider = mongo[this.ApplicationName];
            members = provider.GetCollection<MongoDBMembershipUser>("membership");
            members.MetaData.CreateIndex(new Document("UserName", IndexOrder.Ascending), true);
            members.MetaData.CreateIndex(new Document("Email", IndexOrder.Ascending), false);
        }
        
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        
        public override MembershipUser CreateUser(string username, string password, string email, 
                                                  string passwordQuestion, string passwordAnswer, 
                                                  bool isApproved, object providerUserKey, 
                                                  out MembershipCreateStatus status)
        {
            var user = new MongoDBMembershipUser(){ProviderName=this.Name, Username=username, Password=password,Email=email, 
                                                PasswordQuestion = passwordQuestion, PasswordAnswer = passwordAnswer,
                                                IsApproved = isApproved, IsLockedOut = false, ProviderUserKey=providerUserKey,
                                                LastActivityDate = DateTime.UtcNow,};            
            try{
                members.Insert(user, true);
            }catch(MongoDuplicateKeyException){
                status = MembershipCreateStatus.DuplicateUserName;
            }catch(MongoException){
                status = MembershipCreateStatus.ProviderError;
            }
            status = MembershipCreateStatus.Success;
            return (MembershipUser)user;
        }
        
        protected override byte[] DecryptPassword(byte[] encodedPassword)
        {
            return base.DecryptPassword(encodedPassword);
        }
        
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        protected override byte[] EncryptPassword(byte[] password)
        {
            return base.EncryptPassword(password);
        }
        
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        
        public override bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        private string GetConfigValue(string configValue, string defaultValue){
            if(String.IsNullOrEmpty(configValue)){
              return defaultValue;
            }
    
            return configValue;
        }
    }
}