using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.SecurityContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Marketplace.Infrastructure.SecurityContext.Entities;

    class ClientAppEntityTypeConfiguration : EntityTypeConfiguration<ClientAppModel>
    {
        public ClientAppEntityTypeConfiguration()
        {
            // key and properties
            this.HasKey(ca => ca.Id);

            // associations
            this.HasMany(ca => ca.UserAppTokens)
                .WithRequired(ca => ca.ClientApp)
                .HasForeignKey(ca => ca.ClientAppId)
                .WillCascadeOnDelete(false);

            // to table
            this.ToTable("ClientApps");
        }
    }
}
