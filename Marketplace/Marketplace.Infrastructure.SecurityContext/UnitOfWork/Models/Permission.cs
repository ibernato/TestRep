using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public class PermissionModel
    {
        public PermissionModel()
        {
            UserGroups = new HashSet<UserGroupModel>();
        }

        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public short AccessMode { get; set; }
        
        public ICollection<UserGroupModel> UserGroups { get; set; }
    }
}
