using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Security.Principal;

namespace Marketplace.Security.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Tries to authenticate the user with the given credentials and encrypted security data, and stores
        /// the result in the resulting <see cref="IRepointerPrincipal"/> object.
        /// </summary>
        /// <param name="username">username to check</param>
        /// <param name="clientAppName">client app name to check</param>
        /// <param name="requestTimestamp">timestamp of the request to check</param>
        /// <param name="encryptedRequestData">encryption of the requestData</param>
        /// <param name="requestData">data used in the encryption process</param>
        /// <returns><see cref="IRepointerPrincipal"/> object for the user being authenticated</returns>
        ISystemPrincipal AuthenticateUser(string username, string clientAppName, string requestTimestamp,
                string encryptedRequestData, string requestData);


        /// <summary>
        /// Checks if the user with the provided credentials is identified by the client application 
        /// with the given name.
        /// </summary>
        /// <param name="username">username to check</param>
        /// <param name="password">password to check</param>
        /// <param name="clientAppName">name of the client application the user is using</param>
        /// <returns>True if the user credentials are correct, false otherwise.</returns>
        bool CheckCredentials(string username, string password, string clientAppName);


        /// <summary>
        /// Issues new authorization token for the user with the given username.
        /// </summary>
        /// <param name="userName">Name of the user to issue token for.</param>
        /// <param name="clientAppName">Name of the client application the user is using.</param>
        /// <param name="usePersistentToken">True if persistent token should be used, false otherwise.</param>
        /// <returns>Issued token for the user.</returns>
        string IssueUserToken(string username, string clientAppName, bool usePersistentToken);
    }
}
