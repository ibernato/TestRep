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

    class UserEntityTypeConfiguration : EntityTypeConfiguration<UserModel>
    {
        public UserEntityTypeConfiguration()
        {
            // key and properties
            this.HasKey(u => u.Id);

            // associations
            this.HasMany(e => e.UserAppTokens)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AppUser_Id);

            this.HasOptional(e => e.Contact)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete();

            this.HasMany(e => e.UserGroups)
                .WithMany(e => e.Users)
                .Map(m => m.ToTable("UserUserGroup"));

            // to table
            this.ToTable("Users");
        }
    }
}
