using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;
using Marketplace.Security.Helpers;

namespace Marketplace.Security
{
    public partial class User : AggregateRoot
    {
        #region Constructor

        public User()
        {
            this.LCID = 1033;
        }

        #endregion

        #region Primitive Properties

        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual int LCID { get; set; }

        public virtual Nullable<Guid> BusinessUnitId { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<Guid> UserGroups { get; set; }

        public virtual ICollection<UserAppToken> UserAppTokens { get; set; }

        public virtual Contact Contact { get; set; }

        #endregion

        public UserAppToken ExtendExpirationForUserToken(ClientApp clientApp, double validity, bool usePersistentToken = false)
        {
            var userAppToken = this.UserAppTokens.SingleOrDefault(u => u.ClientAppId == clientApp.Id);

            if (userAppToken == null)
                throw new Exception("@userAppToken");

            // prolong token expiration (but only if more than 25% of the valid time has passed)
            var newTokenExpiration = DateTime.UtcNow.Add(SecurityUtils.CreateTimespan(clientApp.TokenValidity));
            int validityUpdateTimeLimit = Convert.ToInt32(validity * clientApp.TokenValidity);

            if (userAppToken.TokenExpiration == null || (userAppToken.TokenExpiration != null && userAppToken.TokenExpiration < DateTime.UtcNow))
            {
                // create new token
                byte[] tokenData = new byte[32];
                new RNGCryptoServiceProvider().GetBytes(tokenData);
                userAppToken.Token = Convert.ToBase64String(tokenData);
                userAppToken.TokenExpiration = DateTime.UtcNow.Add(usePersistentToken ? SecurityUtils.CreateTimespan(clientApp.PersistentTokenValidity) : SecurityUtils.CreateTimespan(clientApp.TokenValidity));
            }
            else if (userAppToken.TokenExpiration.Value < newTokenExpiration && (userAppToken.TokenExpiration.Value - DateTime.UtcNow) < SecurityUtils.CreateTimespan(validityUpdateTimeLimit))
            {
                userAppToken.TokenExpiration = newTokenExpiration;
            }

            return userAppToken;
        }

        public void InvalidateUserToken(Guid clientAppId)
        {
            // set token to some expired value
            var userAppToken = this.UserAppTokens.SingleOrDefault(u => u.ClientAppId == clientAppId);

            if (userAppToken == null)
                throw new Exception("@userAppToken");

            userAppToken.TokenExpiration = null;
            userAppToken.Token = null;
        }

        public string GetUserToken(Guid clientAppId)
        {
            // set token to some expired value
            var userAppToken = this.UserAppTokens.SingleOrDefault(u => u.ClientAppId == clientAppId);

            if (userAppToken == null)
                return string.Empty;

            var token = userAppToken.Token;

            if (string.IsNullOrEmpty(token) || !userAppToken.TokenExpiration.HasValue || userAppToken.TokenExpiration.Value < DateTime.UtcNow)
                return string.Empty;

            return token;
        }

    }
}
