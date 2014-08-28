using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Marketplace.Infrastructure
{
    [ComplexType]
    public partial class Auditing
    {
        #region Primitive Properties

        public virtual Nullable<System.DateTime> ModifiedOn { get; set; }

        public virtual Nullable<Guid> ModifiedBy { get; set; }

        public virtual Nullable<System.DateTime> CreatedOn { get; set; }

        public virtual Nullable<Guid> CreatedBy { get; set; }

        #endregion
    }
}
