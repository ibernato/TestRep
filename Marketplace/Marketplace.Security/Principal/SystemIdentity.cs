using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Security.Principal
{
    public class SystemIdentity : ISystemIdentity
    {
        public SystemIdentity()
        {
            this.IsAuthenticated = false;
        }

        public SystemIdentity(Guid id, string name, AuthenticationType authenticationType)
        {
            this.Id = id;
            this.Name = name;
            this.AuthenticationType = authenticationType.ToString();
            this.IsAuthenticated = authenticationType != Principal.AuthenticationType.None;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string AuthenticationType { get; private set; }

        public bool IsAuthenticated { get; private set; }
    }

}
