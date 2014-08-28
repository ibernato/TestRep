using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public partial class PermissionModel
    {
        #region Constructor

        public PermissionModel()
        {
            UserGroups = new HashSet<UserGroupModel>();
        }

        #endregion

        #region Primitive Properties

        public virtual Guid Id { get; set; }

        public virtual string TypeName { get; set; }

        public virtual short AccessMode { get; set; }

        #endregion

        #region Complex Properties

        public virtual Auditing AuditInfo { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<UserGroupModel> UserGroups { get; set; }

        #endregion
    }
}
