using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Security
{
    public partial class User : AggregateRoot
    {
        #region Constructor

        public User()
        {
            this.LCID = 1033;
        }

        #endregion

        #region Primitive Properties

        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual int LCID { get; set; }

        public virtual Nullable<Guid> BusinessUnitId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<Guid> UserGroups { get; set; }

        public virtual ICollection<UserAppToken> UserAppTokens { get; set; }

        public virtual Contact Contact { get; set; }

        #endregion
    }
}
