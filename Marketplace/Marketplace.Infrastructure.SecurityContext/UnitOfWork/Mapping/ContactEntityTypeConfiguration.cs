using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.SecurityContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Marketplace.Infrastructure.SecurityContext.Entities;

    class ContactEntityTypeConfiguration : EntityTypeConfiguration<ContactModel>
    {
        public ContactEntityTypeConfiguration()
        {
            // key and properties
            this.HasKey(c => c.Id);

            // to table
            this.ToTable("Contacts");
        }
    }
}
