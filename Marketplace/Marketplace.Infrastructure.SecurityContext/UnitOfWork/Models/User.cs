using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public partial class UserModel
    {
        #region Primitive Properties

        public virtual Guid Id { get; set; }

        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual int LCID { get; set; }

        public virtual Nullable<Guid> BusinessUnitId { get; set; }

        #endregion

        #region Complex Properties

        public virtual Auditing AuditInfo { get; set; }

        #endregion

        #region Navigation Properties

        public virtual BusinessUnitModel BusinessUnit { get; set; }

        public virtual ICollection<UserGroupModel> UserGroups { get; set; }

        public virtual ICollection<UserAppTokenModel> UserAppTokens { get; set; }

        public virtual ContactModel Contact { get; set; }

        #endregion
    }
}
