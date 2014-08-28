using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Security
{
    public partial class BusinessUnit : AggregateRoot
    {
        #region Primitive Properties

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsAdminUnit { get; set; }

        public virtual Nullable<Guid> ParentId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<BusinessUnit> ChildUnits { get; set; }

        #endregion
    }
}
