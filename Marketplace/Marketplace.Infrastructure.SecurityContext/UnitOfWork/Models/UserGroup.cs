using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public partial class UserGroupModel
    {
        #region Constructor

        public UserGroupModel()
        {
            Permissions = new HashSet<PermissionModel>();
            Users = new HashSet<UserModel>();
        }

        #endregion

        #region Primitive Properties

        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsAdminGroup { get; set; }

        #endregion

        #region Complex Properties

        public virtual Auditing AuditInfo { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<PermissionModel> Permissions { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }

        #endregion
    }
}
