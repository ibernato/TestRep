using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Security.Principal
{
    /// <summary>
    /// Enumeration of supported authentication types.
    /// </summary>
    public enum AuthenticationType : short
    {
        None = 0,
        Basic = 1,
        Repointer = 2
    }
}
