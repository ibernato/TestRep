using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Marketplace.Security.Principal
{
    /// <summary>
    /// Defines Repointer security context of the user on whose behalf the code is running.
    /// </summary>
    public interface ISystemPrincipal : IPrincipal
    {
        /// <summary>
        /// Repointer idenitity of the principal.
        /// </summary>
        ISystemIdentity SystemIdentity { get; }

        /// <summary>
        ///Gets or sets localization culture for the principal.
        /// </summary>
        CultureInfo Locale { get; set; }

        /// <summary>
        /// Collection of business units that are accessible by the user.
        /// </summary>
        HashSet<Guid> BusinessUnits { get; }

        /// <summary>
        /// Collection of permissions that the user posseses.
        /// </summary>
        HashSet<string> Permissions { get; }

        /// <summary>
        /// Identification number of the client application from which the user sent the request.
        /// </summary>
        Guid? ClientApplicationId { get; }

        /// <summary>
        /// Checks whether the principal is in the given role.
        /// </summary>
        /// <param name="role">role to check against</param>
        /// <returns>whether the principal is in the given role</returns>
        bool IsInRole(PrincipalRole role);

        /// <summary>
        /// Checks whether principal has access to the given business unit.
        /// </summary>
        /// <param name="businessUnitId">Identification of the business unit whose membership is being checked.</param>
        /// <returns>whether the principal has the appropriate membership</returns>
        bool HasBusinessUnitAccess(Guid businessUnit);

        /// <summary>
        /// Checks whether principal has the given access permission.
        /// </summary>
        /// <param name="typeName">permission to be checked</param>
        /// <returns>whether the principal has the appropriate access permission</returns>
        bool HasPermission(string permission);

    }

}
