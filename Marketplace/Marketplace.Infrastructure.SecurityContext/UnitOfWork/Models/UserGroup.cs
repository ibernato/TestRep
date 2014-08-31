using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public class UserGroupModel : ICreatedAuditing, IModifiedAuditing, IVersioning, IObjectState
    {
        public UserGroupModel()
        {
            Permissions = new HashSet<PermissionModel>();
            Users = new HashSet<UserModel>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAdminGroup { get; set; }


        public ICollection<PermissionModel> Permissions { get; set; }
        public ICollection<UserModel> Users { get; set; }


        public AuditingInfo Created { get; set; }
        public AuditingInfo Modified { get; set; }
        public ObjectState ObjectState { get; set; }
        public Versioning Version { get; set; }
    }
}
