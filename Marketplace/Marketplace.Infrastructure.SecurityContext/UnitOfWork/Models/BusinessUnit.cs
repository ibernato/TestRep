using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public class BusinessUnitModel : ICreatedAuditing, IModifiedAuditing, IVersioning, IObjectState
    {
        public BusinessUnitModel()
        {
            Users = new HashSet<UserModel>();
            ChildUnits = new HashSet<BusinessUnitModel>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAdminUnit { get; set; }

        public Nullable<Guid> ParentId { get; set; }
        public BusinessUnitModel BusinessUnit { get; set; }

        public ICollection<BusinessUnitModel> ChildUnits { get; set; }
        public ICollection<UserModel> Users { get; set; }


        public ObjectState ObjectState { get; set; }
        public Versioning Version { get; set; }
        public AuditingInfo Created { get; set; }
        public AuditingInfo Modified { get; set; }
    }
}
