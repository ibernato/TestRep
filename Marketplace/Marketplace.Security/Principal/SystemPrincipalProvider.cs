using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading;

namespace Marketplace.Security.Principal
{
    [Export(typeof(ISystemPrincipalProvider))]
    public class SystemPrincipalProvider : ISystemPrincipalProvider
    {
        public ISystemPrincipal GetPrincipal()
        {
            var principal = Thread.CurrentPrincipal as ISystemPrincipal;
            if (principal == null)
            {
                principal = new SystemPrincipal(PrincipalRole.Anonymous);
                SetPrincipal(principal);
            }

            return principal;
        }

        public ISystemPrincipal CreateSystemPrincipal()
        {
            return new SystemPrincipal(PrincipalRole.System);
        }

        public void SetPrincipal(ISystemPrincipal principal)
        {
            if (principal == null)
            {
                principal = new SystemPrincipal();
            }

            Thread.CurrentPrincipal = principal;
        }

        public ISystemPrincipal SetPrincipal(Guid id, string name, int lcid, AuthenticationType authenticationType,
                Guid? clientApplicationId, IEnumerable<Guid> accessibleBusinessUnits, IEnumerable<string> permissions)
        {
            ISystemPrincipal principal = new SystemPrincipal(id, name, lcid, authenticationType, clientApplicationId, accessibleBusinessUnits, permissions);

            SetPrincipal(principal);

            return principal;
        }

    }

}
