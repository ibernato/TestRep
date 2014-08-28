using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Infrastructure.Repository;
using Marketplace.Infrastructure.SecurityContext.UnitOfWork;
using Marketplace.Security;
using Marketplace.Security.Contracts;
using Marketplace.Core;

namespace Marketplace.Infrastructure.SecurityContext.Repositories
{
    [Export(typeof(IBusinessUnitRepository))]
    public class BusinessUnitRepository : Repository<BusinessUnit, Marketplace.Infrastructure.SecurityContext.Entities.BusinessUnitModel, Guid>, IBusinessUnitRepository
    {
        #region Constructor

        [ImportingConstructor]
        public BusinessUnitRepository(SecurityUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }

        #endregion

        public IEnumerable<Guid> FindNonAdminBusinessUnits()
        {
            return base.AllMatching(bu => !bu.IsAdminUnit).Select(bu => bu.Id);
        }

        public override BusinessUnit Get(Guid key)
        {
            return All().SingleOrDefault(bu => bu.Id == key).As<BusinessUnit>();
        }

        internal override IEnumerable<string> Includes
        {
            get { return new List<string>() { "BusinessUnits" }; }
        }

    }
}
