using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public partial class UserAppTokenModel
    {
        #region Primitive Properties

        public virtual Guid Id { get; set; }

        public virtual Guid ClientAppId { get; set; }

        public virtual string Token { get; set; }

        public virtual Nullable<System.DateTime> TokenExpiration { get; set; }

        public virtual Guid AppUser_Id { get; set; }

        #endregion

        #region Navigation Properties

        public virtual UserModel User { get; set; }

        public virtual ClientAppModel ClientApp { get; set; }

        #endregion

    }
}
