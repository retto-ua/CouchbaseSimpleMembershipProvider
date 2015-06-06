using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Hosting;
using System.Web.Security;
using WebMatrix.WebData; 

namespace Retto.CouchbaseSimpleMembershipProvider {

    public class CouchbaseMembershipProvider : ExtendedMembershipProvider {
        private string _connectionString;

        #region Membership Provider properties

        public override bool EnablePasswordRetrieval {
            get { return _enablePasswordRetrieval; }
        }
        private bool _enablePasswordRetrieval;

        public override bool EnablePasswordReset {
            get { return _enablePasswordReset; }
        }
        private bool _enablePasswordReset;

        public override bool RequiresQuestionAndAnswer {
            get { return _requiresQuestionAndAnswer; }
        }
        private bool _requiresQuestionAndAnswer;

        public override string ApplicationName {
            get { return _applicationName; }
            set { _applicationName = value; }
        }
        private string _applicationName;

        public override int MaxInvalidPasswordAttempts {
            get { return _maxInvalidPasswordAttempts; }
        }
        private int _maxInvalidPasswordAttempts;

        public override int PasswordAttemptWindow {
            get { return _passwordAttemptWindow; }
        }
        private int _passwordAttemptWindow;

        public override bool RequiresUniqueEmail {
            get { return _requiresUniqueEmail; }
        }
        private bool _requiresUniqueEmail;

        public override MembershipPasswordFormat PasswordFormat {
            get { return _passwordFormat; }
        }
        private MembershipPasswordFormat _passwordFormat;

        public override int MinRequiredPasswordLength {
            get { return _minRequiredPasswordLength; }
        }
        private int _minRequiredPasswordLength;

        public override int MinRequiredNonAlphanumericCharacters {
            get { return _minRequiredNonAlphanumericCharacters; }
        }
        private int _minRequiredNonAlphanumericCharacters;

        public override string PasswordStrengthRegularExpression {
            get { return _passwordStrengthRegularExpression; }
        }
        private string _passwordStrengthRegularExpression;

        #endregion

        public CouchbaseMembershipProvider() { }

        #region Initialization

        public override void Initialize(string name, NameValueCollection config) {
            DoPreValidation(name, config);

            base.Initialize(name, config);

            RetrieveConfigurationValues(config);

            RetrievePasswordFormat(config);

            RetrieveConnectionString(config);
        }

        private void DoPreValidation(string name, NameValueCollection config) {
            if (config == null) { throw new ArgumentNullException("config"); }
            if (string.IsNullOrEmpty(name)) { name = "CouchbaseMembershipProvider"; }
            if (string.IsNullOrEmpty(config["description"])) {
                config.Remove("description");
                config.Add("description", "Couchbase application");
            }
        }

        private void RetrieveConfigurationValues(NameValueCollection config) {

            _applicationName = GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);

            if (_applicationName.Length > 256) {
                throw new ProviderException("Too long application name");
            }

            _maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));

            _passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));

            _minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));

            _minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));

            _passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));

            _enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "True"));

            _enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "False"));

            _requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "False"));

            _requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "True"));
        }

        private void RetrievePasswordFormat(NameValueCollection config) {
            string format = config["passwordFormat"];

            format = format == null ? "hashed" : format.ToLowerInvariant();

            switch (format) {
                case "hashed": { _passwordFormat = MembershipPasswordFormat.Hashed; }
                break;
                case "encrypted": { _passwordFormat = MembershipPasswordFormat.Encrypted; }
                break;
                case "clear": { _passwordFormat = MembershipPasswordFormat.Clear; }
                break;
                default: { throw new ProviderException("Password format not supported."); }
            }
        }

        private void RetrieveConnectionString(NameValueCollection config) {
            string connectionStringName = config["connectionStringName"];

            if (string.IsNullOrEmpty(connectionStringName))
                throw new ProviderException("Connection name is not specified");

            _connectionString = GetConnectionString(connectionStringName);

            if (string.IsNullOrEmpty(_connectionString)) {
                throw new ProviderException("Connection_string_not_found");
            }
        }

        #endregion

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer) {
            throw new NotImplementedException();
        }
         
        public override bool ChangePassword(string username, string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user) {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password) {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName) {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline) {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email) {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline() {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName) {
            throw new NotImplementedException();
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values) {
            throw new NotImplementedException();
        }

        public override string CreateAccount(string userName, string password, bool requireConfirmationToken) {
            throw new NotImplementedException();
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken) {
            throw new NotImplementedException();
        }

        public override bool ConfirmAccount(string accountConfirmationToken) {
            throw new NotImplementedException();
        }

        public override bool DeleteAccount(string userName) {
            throw new NotImplementedException();
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow) {
            throw new NotImplementedException();
        }

        public override int GetUserIdFromPasswordResetToken(string token) {
            throw new NotImplementedException();
        }

        public override bool IsConfirmed(string userName) {
            throw new NotImplementedException();
        }

        public override bool ResetPasswordWithToken(string token, string newPassword) {
            throw new NotImplementedException();
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName) {
            throw new NotImplementedException();
        }

        public override DateTime GetCreateDate(string userName) {
            throw new NotImplementedException();
        }

        public override DateTime GetPasswordChangedDate(string userName) {
            throw new NotImplementedException();
        }

        public override DateTime GetLastPasswordFailureDate(string userName) {
            throw new NotImplementedException();
        }

        #region Helper methods

        private static string GetConfigValue(string configValue, string defaultValue) {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        private static string GetConnectionString(string specifiedConnectionString) {
            if (string.IsNullOrEmpty(specifiedConnectionString)) { return null; }

            string connectionString = null;

            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[specifiedConnectionString];
            if (connectionStringSettings != null) { connectionString = connectionStringSettings.ConnectionString; }

            return connectionString;
        }

        #endregion
    }
}
