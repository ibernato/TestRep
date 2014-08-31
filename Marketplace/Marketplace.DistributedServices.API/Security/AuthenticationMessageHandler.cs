using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Marketplace.Security.Authentication;
using Marketplace.Security.Principal;
using Newtonsoft.Json;

namespace Marketplace.DistributedServices.API.Security
{
    [Export]
    public class AuthenticationMessageHandler : DelegatingHandler
    {
        private const string AuthenticationTypeConfigKey = "AuthenticationType";
        private const string AuthResponseHeader = "WWW-Authenticate";
        private const string AuthResponseTokenExpiration = "Rep-TokenExpiration";

        [Import]
        public IAuthenticationService authenticationService { get; set; }

        [Import]
        public ISystemPrincipalProvider principalProvider { get; set; }

        private AuthenticationType appAuthType { get { return AuthenticationType.Repointer; } }

        /// <summary>
        /// Before it sends an HTTP request to the inner handler to send to the server as an asynchronous operation,
        /// it first sets the principal on the thread so that AuthorizeAttribute can properly decide whether user is
        /// authenticated.
        /// </summary>
        /// <param name="request">Http request message</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>task object</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authHeaderValue = request.Headers.Authorization;

            authenticationService.CheckCredentials("test", "", "");

            // set default configured authentication type
            AuthenticationType authType = this.appAuthType;
            ISystemPrincipal principal = null;
            bool isAuthTypeValid = false;

            if (authHeaderValue != null && !String.IsNullOrWhiteSpace(authHeaderValue.Parameter))
            {
                // try to set the authentication type from the request header
                if (Enum.TryParse<AuthenticationType>(authHeaderValue.Scheme, out authType))
                {
                    if (authType == AuthenticationType.Repointer)
                    {
                        isAuthTypeValid = true;

                        string[] credentials = Encoding.ASCII.GetString(
                                Convert.FromBase64String(authHeaderValue.Parameter)).Split(':');

                        if (credentials.Length == 4 &&
                                !string.IsNullOrEmpty(credentials[0]) && !string.IsNullOrEmpty(credentials[1]) &&
                                !string.IsNullOrEmpty(credentials[2]) && !string.IsNullOrEmpty(credentials[3]))
                        {
                            var username = credentials[1];
                            var clientAppName = credentials[0];
                            var requestTimestamp = credentials[3];
                            var encryptedRequestData = credentials[2];
                            var requestData = Uri.EscapeDataString(HttpUtility.UrlDecode(request.RequestUri.AbsoluteUri));

                            principal = authenticationService.AuthenticateUser(username, clientAppName,
                                    requestTimestamp, encryptedRequestData, requestData);
                        }
                    }
                    else
                    {
                        // NOTE: Remove AuthorizeAttribute from controllers in order to use them by unauthenticated users.
                        principal = null;
                    }
                }
            }

            // if principal is null, it will be set as anonymous
            principalProvider.SetPrincipal(principal);
            principal = principalProvider.GetPrincipal();

            // set the principal in the context also, so that security context does not become inconsistent
            if (HttpContext.Current != null)
                HttpContext.Current.User = principal;

            // send to base delegate method to performs whatever is requested
            var response = await base.SendAsync(request, cancellationToken);

            // fix response header if unauthorized
            if (response.StatusCode == HttpStatusCode.Unauthorized && !response.Headers.Contains(AuthResponseHeader))
            {
                var authTypeString = isAuthTypeValid ? authType.ToString() : this.appAuthType.ToString();
                response.Headers.Add(AuthResponseHeader, authTypeString);
            }

            return response;
        }
    }
}