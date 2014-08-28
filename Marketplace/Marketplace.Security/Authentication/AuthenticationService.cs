using System;
using System.Collections.Generic;
using System.Composition;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Marketplace.Core;
using Marketplace.Security.Contracts;
using Marketplace.Security.Helpers;
using Marketplace.Security.Principal;

namespace Marketplace.Security.Authentication
{
    [Export(typeof(IAuthenticationService))]
    public class AuthenticationService : IAuthenticationService
    {
        private static readonly DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly double ValidityTimespanFactor = 0.75;

        private CryptoService cryptoService;

        private IClientAppRepository clientAppRepository;
        private IUserRepository userRepository;
        private IUserGroupRepository userGroupRepository;
        private IBusinessUnitRepository businessUnitRepository;

        [Import]
        public ISystemPrincipalProvider PrincipalProvider { get; set; }

        #region Constructors

        [ImportingConstructor]
        public AuthenticationService(RepositoryFactory<IClientAppRepository> clientAppRepositoryFactory,
            RepositoryFactory<IUserRepository> userRepositoryFactory,
            RepositoryFactory<IBusinessUnitRepository> businessUnitRepositoryFactory,
            RepositoryFactory<IUserGroupRepository> userGroupRepositoryFactory,
            [Import("SecurityContext")]IUnitOfWork unitOfWork)
        {
            this.cryptoService = new CryptoService();
            this.userRepository = userRepositoryFactory.Create(unitOfWork);
            this.clientAppRepository = clientAppRepositoryFactory.Create(unitOfWork);
            this.businessUnitRepository = businessUnitRepositoryFactory.Create(unitOfWork);
            this.userGroupRepository = userGroupRepositoryFactory.Create(unitOfWork);
        }

        #endregion

        #region IAuthenticationService

        public bool CheckCredentials(string username, string password, string clientAppName)
        {
            var user = GetUserByUsername(username);

            if (user == null)
                return false;

            //var usernew = new User()
            //{
            //    Contact = new Contact()
            //    {
            //        Email = "test email",
            //        Name = "test kontakt",
            //        Tel = "neki telefon"
            //    },
            //    LCID = 1050,
            //    Username = "test username"
            //};
            //usernew.GenerateNewIdentity();
            //userRepository.Create(usernew);


            //var usergroup = new UserGroup()
            //{
            //    Description = "prva",
            //    IsAdminGroup = true,
            //    Name = "prva"
            //};
            //usergroup.GenerateNewIdentity();
            //userGroupRepository.Create(usergroup);


            var usere = userRepository.Get(new Guid("C9E8F329-D0FF-C76A-2A76-08D190D534F2"));

            usere.Contact.Email = "3";
            usere.UserGroups.Add(new Guid("BD89E744-705B-CAE4-DCD1-08D190DA71F2"));

            userRepository.Update(usere);


            //var ca = new ClientApp() { ClientToken = "fffffffff", Name = "test" };
            //ca.GenerateNewIdentity();
            //clientAppRepository.Create(ca);

            userRepository.UnitOfWork.Commit();

            try
            {
                CheckClientApp(user.Id, clientAppName);
            }
            catch (InvalidCastException exception) //ClientApplicationAccessViolationException
            {
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }

            var passwordOk = user.Password == cryptoService.Compute(password, user.PasswordSalt);

            return passwordOk;
        }

        public ISystemPrincipal AuthenticateUser(string username, string clientAppName, string requestTimestamp, string encryptedRequestData, string requestData)
        {
            var user = GetUserByUsername(username);

            if (user == null)
                return new SystemPrincipal();

            ClientApp clientApp;

            try
            {
                clientApp = CheckClientApp(user.Id, clientAppName);
            }
            catch (InvalidCastException exception) //ClientApplicationAccessViolationException
            {
                return new SystemPrincipal();
            }
            catch (Exception ex)
            {
                throw;
            }

            if (!ValidateTimestamp(requestTimestamp, clientApp.RequestTimeOffset))
                return new SystemPrincipal();

            var token = GetUserToken(user.Id, clientApp.Id);
            if (string.IsNullOrEmpty(token))
                return new SystemPrincipal();

            if (encryptedRequestData != HashHMACSHA1(clientApp.ClientToken, token, requestData))
                return new SystemPrincipal();

            var permissions = TransformPermissionsDictionaryToStringList(GetUserPermissions(user.Id));
            var accessibleBusinessUnits = GetUserAccessibleBusinessUnits(user.Id);

            ExtendExpirationForUserToken(clientApp, user);

            return new SystemPrincipal(user.Id, username, user.LCID, AuthenticationType.Repointer, clientApp.Id,
                    accessibleBusinessUnits, permissions);
        }

        public string IssueUserToken(string username, string clientAppName, bool usePersistentToken)
        {
            var user = GetUserByUsername(username);
            if (user == null)
            {
                throw new Exception("@user");
            }

            ClientApp clientApp = null;

            try
            {
                clientApp = CheckClientApp(user.Id, clientAppName);
            }
            catch (InvalidCastException exception) //ClientApplicationAccessViolationException
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw;
            }

            var userAppToken = ExtendExpirationForUserToken(clientApp, user, usePersistentToken);

            return userAppToken.Token;
        }

        public void InvalidateUserToken(Guid userId, Guid clientAppId)
        {
            var user = userRepository.Get(userId);

            if (user == null)
                throw new Exception("@user");

            // set token to some expired value
            var userAppToken = user.UserAppTokens.SingleOrDefault(u => u.ClientAppId == clientAppId);

            if (userAppToken == null)
                throw new Exception("@userAppToken");

            userAppToken.TokenExpiration = null;
            userAppToken.Token = null;

            userRepository.Update(user);
        }

        public string GetUserToken(Guid userId, Guid clientAppId)
        {
            var user = userRepository.Get(userId);

            if (user == null)
                return string.Empty;

            // set token to some expired value
            var userAppToken = user.UserAppTokens.SingleOrDefault(u => u.ClientAppId == clientAppId);

            if (userAppToken == null)
                return string.Empty;

            var token = userAppToken.Token;

            if (string.IsNullOrEmpty(token) || !userAppToken.TokenExpiration.HasValue || userAppToken.TokenExpiration.Value < DateTime.UtcNow)
                return string.Empty;

            return token;
        }

        public DateTime GetUserTokenExpiration()
        {
            var currentPrincipal = PrincipalProvider.GetPrincipal();

            if (currentPrincipal.IsInRole(PrincipalRole.Anonymous))
                return DateTime.MinValue;

            var user = userRepository.Get(currentPrincipal.SystemIdentity.Id);
            if (user == null)
                return DateTime.MinValue;

            var clientAppId = currentPrincipal.ClientApplicationId;

            // set token to some expired value
            var userAppToken = user.UserAppTokens.SingleOrDefault(u => u.ClientAppId == clientAppId);
            if (userAppToken == null)
                return DateTime.MinValue;

            return userAppToken.TokenExpiration.HasValue ? userAppToken.TokenExpiration.Value : DateTime.MinValue;
        }

        #endregion

        #region Private Methods

        private ClientApp GetClientAppByName(string clientAppName)
        {
            return clientAppRepository.GetClientAppByName(clientAppName);
        }

        private User GetUserByUsername(string userName)
        {
            return userRepository.GetUserByUsername(userName);
        }

        private ClientApp CheckClientApp(Guid userId, string clientAppName)
        {
            var user = userRepository.Get(userId);

            if (user == null)
                throw new Exception("@user");

            var clientApp = GetClientAppByName(clientAppName);

            if (clientApp == null)
                throw new Exception("@clientApp");

            if (!user.UserAppTokens.Select(p => p.ClientAppId).Contains(clientApp.Id))
                throw new Exception("@token");

            return clientApp;
        }

        private string HashHMACSHA1(string clientToken, string userToken, string data)
        {
            var key = String.Concat(clientToken, userToken);

            byte[] keyByte = new System.Text.ASCIIEncoding().GetBytes(key);
            HMACSHA1 myhmacsha1 = new HMACSHA1(keyByte);
            byte[] byteArray = Encoding.ASCII.GetBytes(data);

            var hash = myhmacsha1.ComputeHash(byteArray)
                        .Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);

            return hash;
        }

        private bool ValidateTimestamp(string milliseconds, int requestOffsetTime)
        {
            double timestamp;

            if (!Double.TryParse(milliseconds, NumberStyles.Any, CultureInfo.InvariantCulture, out timestamp))
                return false;

            var currentTimeMiliseconds = (DateTime.UtcNow - Jan1970).TotalMilliseconds;
            var milisecondsDifference = Math.Abs(timestamp - currentTimeMiliseconds);

            return (TimeSpan.FromMilliseconds(milisecondsDifference).TotalSeconds <= requestOffsetTime);
        }

        private Dictionary<string, ICollection<string>> GetUserPermissions(Guid userId)
        {
            var user = userRepository.Get(userId);
            var userGroups = userGroupRepository.FindAllUserGroupsWithId(user.UserGroups.ToArray());
            var permissions = new Dictionary<string, ICollection<string>>();

            var isUserInAdminGroup = userGroups.Any(ug => ug.IsAdminGroup == true);

            var allPermissions = userGroups.SelectMany(ug => ug.Permissions).Distinct();

            foreach (var userGroupPermission in allPermissions)
            {
                if (!permissions.ContainsKey(userGroupPermission.TypeName))
                {
                    permissions.Add(userGroupPermission.TypeName, new List<string>());
                }

                permissions.SingleOrDefault(p => p.Key == userGroupPermission.TypeName)
                        .Value.Add(userGroupPermission.AccessMode.ToString());
            }
            
            return permissions;
        }

        private Dictionary<string, ICollection<string>> GetUserPermissions()
        {
            var currentPrincipal = PrincipalProvider.GetPrincipal();
            return GetUserPermissions(currentPrincipal.SystemIdentity.Id);
        }

        private IEnumerable<Guid> GetUserAccessibleBusinessUnits(Guid userId)
        {
            var user = userRepository.Get(userId);

            if (user == null)
            {
                throw new Exception("@user");
            }

            BusinessUnit businessUnit = null;
            HashSet<Guid> accessibleUnits = new HashSet<Guid>();

            if (user.BusinessUnitId.HasValue)
            {
                businessUnit = businessUnitRepository.Get(user.BusinessUnitId.Value);

                var isUserInAdminUnit = businessUnit != null && businessUnit.IsAdminUnit;
                if (isUserInAdminUnit)
                {
                    // if user is in admin unit, he is still not allowed to see information bound to higher admin units
                    // (since admin unit can also be hierarchical)GetFiltered(bu => !bu.IsAdminUnit).Select(bu => bu.Id)
                    accessibleUnits = new HashSet<Guid>(businessUnitRepository.FindNonAdminBusinessUnits());
                }
            }

            return accessibleUnits;
        }

        private void FindAccessibleBusinessUnits(BusinessUnit businessUnit, ref HashSet<Guid> accessibleBUnits, IUnitOfWork uow)
        {
            if (businessUnit == null)
                return;

            accessibleBUnits.Add(businessUnit.Id);

            foreach (var childUnit in businessUnit.ChildUnits)
            {
                var unitWithChildren = businessUnitRepository.Get(childUnit.Id);
                FindAccessibleBusinessUnits(unitWithChildren, ref accessibleBUnits, uow);
            }
        }

        private IEnumerable<string> TransformPermissionsDictionaryToStringList(Dictionary<string, ICollection<string>> permissionsDictionary)
        {
            var permissionsList = new List<string>();

            foreach (var permission in permissionsDictionary)
            {
                foreach (var permissionValue in permission.Value)
                {
                    var permissionString = SecurityUtils.CreatePermissionAuthString(
                            permission.Key, permissionValue);
                    permissionsList.Add(permissionString);
                }
            }

            return permissionsList;
        }

        private UserAppToken ExtendExpirationForUserToken(ClientApp clientApp, User user, bool usePersistentToken = false)
        {
            var userAppToken = user.UserAppTokens.SingleOrDefault(u => u.ClientAppId == clientApp.Id);

            if (userAppToken == null)
                throw new Exception("@userAppToken");

            // prolong token expiration (but only if more than 25% of the valid time has passed)
            var newTokenExpiration = DateTime.UtcNow.Add(SecurityUtils.CreateTimespan(clientApp.TokenValidity));
            int validityUpdateTimeLimit = Convert.ToInt32(ValidityTimespanFactor * clientApp.TokenValidity);

            if (userAppToken.TokenExpiration == null || (userAppToken.TokenExpiration != null && userAppToken.TokenExpiration < DateTime.UtcNow))
            {
                // create new token
                byte[] tokenData = new byte[32];
                new RNGCryptoServiceProvider().GetBytes(tokenData);
                userAppToken.Token = Convert.ToBase64String(tokenData);
                userAppToken.TokenExpiration = DateTime.UtcNow.Add(usePersistentToken ? SecurityUtils.CreateTimespan(clientApp.PersistentTokenValidity) : SecurityUtils.CreateTimespan(clientApp.TokenValidity));

                userRepository.Update(user);
            }
            else if (userAppToken.TokenExpiration.Value < newTokenExpiration && (userAppToken.TokenExpiration.Value - DateTime.UtcNow) < SecurityUtils.CreateTimespan(validityUpdateTimeLimit))
            {
                userAppToken.TokenExpiration = newTokenExpiration;

                userRepository.Update(user);
            }

            return userAppToken;
        }

        #endregion
    }
}
