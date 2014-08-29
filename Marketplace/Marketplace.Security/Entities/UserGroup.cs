using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Security
{
    public partial class UserGroup : AggregateRoot
    {
        #region Primitive Properties

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsAdminGroup { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<Permission> Permissions { get; set; }

        public virtual ICollection<User> Users { get; set; }

        #endregion
    }
}
