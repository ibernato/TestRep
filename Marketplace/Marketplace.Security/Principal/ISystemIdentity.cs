using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Marketplace.Security.Principal
{
    /// <summary>
    /// Specifies identity information of the user of Repointer application.
    /// </summary>
    public interface ISystemIdentity : IIdentity
    {
        /// <summary>
        /// Id of the current user.
        /// </summary>
        Guid Id { get; }
    }

}
