using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.SecurityContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using Marketplace.Infrastructure.SecurityContext.Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;

    class BusinessUnitEntityTypeConfiguration : EntityTypeConfiguration<BusinessUnitModel>
    {
        public BusinessUnitEntityTypeConfiguration()
        {
            // key and properties
            this.HasKey(bu => bu.Id);

            // associations
            this.HasMany(e => e.Users)
                .WithOptional(e => e.BusinessUnit)
                .HasForeignKey(e => e.BusinessUnitId);

            this.HasMany(e => e.ChildUnits)
                .WithOptional(e => e.BusinessUnit)
                .HasForeignKey(e => e.ParentId);

            // to table
            this.ToTable("BusinessUnits");
        }
    }
}
