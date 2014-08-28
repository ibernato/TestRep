using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.SecurityContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Marketplace.Infrastructure.SecurityContext.Entities;

    /// <summary>
    /// The entity type configuration
    /// </summary>
    class UserGroupEntityTypeConfiguration
        : EntityTypeConfiguration<UserGroupModel>
    {
        public UserGroupEntityTypeConfiguration()
        {
            //key and properties
            this.HasKey(ug => ug.Id);

            this.ToTable("UserGroups");
        }
    }
}
