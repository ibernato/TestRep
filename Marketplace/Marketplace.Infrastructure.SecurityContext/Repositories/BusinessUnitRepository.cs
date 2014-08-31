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
using System.Data.Entity;
using Marketplace.Infrastructure.SecurityContext.Entities;
using RefactorThis.GraphDiff;

namespace Marketplace.Infrastructure.SecurityContext.Repositories
{
    [Export(typeof(IBusinessUnitRepository))]
    public class BusinessUnitRepository : Repository<BusinessUnit, BusinessUnitModel, Guid>, IBusinessUnitRepository
    {
        #region Constructor

        [ImportingConstructor]
        public BusinessUnitRepository(SecurityUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }

        #endregion

        #region Overrides

        private DbContext Context
        {
            get
            {
                return (base.UnitOfWork as DbContext);
            }
        }

        public override void Update(BusinessUnit item)
        {
            Context.UpdateGraph<BusinessUnitModel>(item.As<BusinessUnitModel>().ApplyVersion().ApplyModifiedAudit().ChangeObjectState(),
                b => b.OwnedCollection(c => c.ChildUnits));
        }

        public override BusinessUnit Get(Guid key)
        {
            return All().SingleOrDefault(bu => bu.Id == key).As<BusinessUnit>();
        }

        internal override IEnumerable<string> Includes
        {
            get { return new List<string>() { "BusinessUnits" }; }
        }

        #endregion

        #region IBusinessUnitRepository

        public IEnumerable<Guid> FindNonAdminBusinessUnits()
        {
            return base.AllMatching(bu => !bu.IsAdminUnit).Select(bu => bu.Id);
        }

        #endregion

    }
}
