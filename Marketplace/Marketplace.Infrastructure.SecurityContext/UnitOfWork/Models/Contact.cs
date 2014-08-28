using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public partial class ContactModel
    {
        #region Primitive Properties

        public virtual Guid Id { get; set; }

        public virtual string Tel { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        #endregion

        #region Navigation Properties

        public virtual UserModel User { get; set; }

        #endregion
    }
}
