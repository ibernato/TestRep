using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Security
{
    public partial class UserAppToken : ValueObject<UserAppToken>
    {
        #region Primitive Properties

        public virtual Guid Id { get; set; }

        public virtual Guid ClientAppId { get; set; }

        public virtual string Token { get; set; }

        public virtual Nullable<System.DateTime> TokenExpiration { get; set; }

        #endregion
    }
}
