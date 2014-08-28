using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Security
{
    public partial class Contact : ValueObject<Contact>
    {
        #region Primitive Properties

        public virtual string Tel { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        #endregion
    }
}
