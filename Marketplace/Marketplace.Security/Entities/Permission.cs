using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Security
{
    public enum PermissionAccessMode : short
    {
        View = 0,
        Modify = 1,
    }

    public partial class Permission : ValueObject<Permission>
    {
        #region Primitive Properties

        public virtual string TypeName { get; set; }

        #endregion

        #region Enum Properties

        public virtual PermissionAccessMode AccessMode { get; set; }

        #endregion
    }
}
