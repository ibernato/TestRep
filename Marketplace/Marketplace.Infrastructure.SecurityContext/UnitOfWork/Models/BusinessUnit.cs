using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public partial class BusinessUnitModel
    {
        #region Constructor

        public BusinessUnitModel()
        {
            Users = new HashSet<UserModel>();
            ChildUnits = new HashSet<BusinessUnitModel>();
        }

        #endregion

        #region Primitive Properties

        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsAdminUnit { get; set; }

        public virtual Nullable<Guid> ParentId { get; set; }

        #endregion

        #region Complex Properties

        public virtual Auditing AuditInfo { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<BusinessUnitModel> ChildUnits { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }

        #endregion

        public virtual BusinessUnitModel BusinessUnit { get; set; }
    }
}
