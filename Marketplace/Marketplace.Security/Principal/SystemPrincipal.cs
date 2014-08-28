using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Marketplace.Security.Principal
{
    /// <summary>
    /// Principal user on whose behalf the code is running.
    /// </summary>
    public class SystemPrincipal : ISystemPrincipal
    {
        private PrincipalRole role;

        /// <summary>
        /// Constructor for anonymous principal.
        /// </summary>
        public SystemPrincipal()
            : this(PrincipalRole.Anonymous)
        {

        }

        /// <summary>
        /// Constructor used for either anonymous or sytem role.
        /// </summary>
        /// <param name="role">role of the principal</param>
        public SystemPrincipal(PrincipalRole role)
        {
            if (role == PrincipalRole.System)
            {
                this.role = role;
            }

            SystemIdentity = new SystemIdentity();
            BusinessUnits = new HashSet<Guid>();
            Permissions = new HashSet<string>();
        }

        /// <summary>
        /// RepointerPrincipal constructor.
        /// </summary>
        /// <param name="id">ID of the principal</param>
        /// <param name="name">name of the principal</param>
        /// <param name="lcid">locale identificator</param>
        /// <param name="authenticationType">authentication type used for the principal</param>
        /// <param name="clientAppId">client app that principal is using to send the request</param>
        /// <param name="accessibleBusinessUnits">collection of business units accessible to the principal</param>
        /// <param name="permissions">permissions for the principal</param>
        public SystemPrincipal(Guid id, string name, int lcid, AuthenticationType authenticationType, Guid? clientAppId,
                IEnumerable<Guid> accessibleBusinessUnits, IEnumerable<string> permissions)
        {
            SystemIdentity = new SystemIdentity(id, name, authenticationType);
            BusinessUnits = new HashSet<Guid>(accessibleBusinessUnits);
            Permissions = new HashSet<string>(permissions);
            ClientApplicationId = clientAppId;
            role = PrincipalRole.User;

            Locale = CultureInfo.GetCultureInfo(lcid);
        }

        public CultureInfo Locale
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }

            set
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = value;
            }
        }

        public ISystemIdentity SystemIdentity { get; private set; }

        public IIdentity Identity
        {
            get { return SystemIdentity; }
        }

        public HashSet<Guid> BusinessUnits { get; private set; }

        public HashSet<string> Permissions { get; private set; }

        public Guid? ClientApplicationId { get; private set; }

        public bool IsInRole(string role)
        {
            return IsInRole((PrincipalRole)Enum.Parse(typeof(PrincipalRole), role, true));
        }

        public bool IsInRole(PrincipalRole role)
        {
            return this.role == role;
        }

        public bool HasBusinessUnitAccess(Guid businessUnit)
        {
            return IsInRole(PrincipalRole.System) || BusinessUnits.Contains(businessUnit);
        }

        public bool HasPermission(string permission)
        {
            return IsInRole(PrincipalRole.System) || Permissions.Contains(permission);
        }

    }

}
