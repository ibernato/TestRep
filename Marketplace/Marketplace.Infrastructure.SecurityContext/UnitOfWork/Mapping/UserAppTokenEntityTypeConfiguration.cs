using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.SecurityContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Marketplace.Infrastructure.SecurityContext.Entities;
    using Marketplace.Security;

    class UserAppTokenEntityTypeConfiguration : EntityTypeConfiguration<UserAppTokenModel>
    {
        public UserAppTokenEntityTypeConfiguration()
        {
            // key and properties
            this.HasKey(uat => uat.Id);

            // to table
            this.ToTable("UserAppTokens");
        }
    }
}
