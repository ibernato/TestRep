using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.SecurityContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Marketplace.Infrastructure.SecurityContext.Entities;

    class PermissionEntityTypeConfiguration : EntityTypeConfiguration<PermissionModel>
    {
        public PermissionEntityTypeConfiguration()
        {
            // key and properties
            this.HasKey(p => p.Id);

            // associations
            this.HasMany(e => e.UserGroups)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("UserGroupPermission"));

            // to table
            this.ToTable("Permissions");
        }
    }
}
