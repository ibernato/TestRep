using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Security.Principal
{
    /// <summary>
    /// Defines provider for manipulating with <see cref="IRepointerPrincipal" /> object.
    /// </summary>
    public interface ISystemPrincipalProvider
    {
        /// <summary>
        /// Gets the current <see cref="IRepointerPrincipal"/> object. If it's null, it's considered as anonymous.
        /// </summary>
        /// <returns><see cref="IRepointerPrincipal"/> object.</returns>
        ISystemPrincipal GetPrincipal();

        /// <summary>
        /// Creates system principal that has all admin permissions, but no real identity.
        /// </summary>
        /// <returns><see cref="IRepointerPrincipal"/> object.</returns>
        ISystemPrincipal CreateSystemPrincipal();

        /// <summary>
        /// Sets the current <see cref="IRepointerPrincipal"/> object to the given value. If it's null, it will be
        /// set as anonymous.
        /// </summary>
        /// <param name="principal">Principal to be set.</param>
        void SetPrincipal(ISystemPrincipal principal);

        /// <summary>
        /// Creates new <see cref="IRepointerPrincipal"/> object based on the given values and sets it as 
        /// the current principal.
        /// </summary>
        /// <param name="id">identification number of the principal</param>
        /// <param name="name">name of the principal</param>
        /// <param name="lcid">locale identification</param>
        /// <param name="authenticationType">type of authentication used to authenticate the principal</param>
        /// <param name="clientApplicationId">identificaiton of the client application the principal is using</param>
        /// <param name="accessibleBusinessUnits">collection of business units to which the principle has access</param>
        /// <param name="permissions">collection of permissions</param>
        /// <returns>newly created <see cref="IRepointerPrincipal"/> object</returns>
        ISystemPrincipal SetPrincipal(Guid id, string name, int lcid, AuthenticationType authenticationType,
                Guid? clientApplicationId, IEnumerable<Guid> accessibleBusinessUnits, IEnumerable<string> permissions);

    }

}
