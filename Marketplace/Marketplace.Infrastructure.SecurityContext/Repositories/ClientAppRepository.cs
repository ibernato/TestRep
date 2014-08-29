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
    [Export(typeof(IClientAppRepository))]
    public class ClientAppRepository : Repository<ClientApp, ClientAppModel, Guid>, IClientAppRepository
    {
        #region Constructor

        [ImportingConstructor]
        public ClientAppRepository(SecurityUnitOfWork unitOfWork)
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

        public override void Update(ClientApp item)
        {
            Context.UpdateGraph<ClientAppModel>(item.As<ClientAppModel>().ApplyVersion().ApplyModifiedAudit().ChangeObjectState(), 
                m => m.AssociatedCollection(c => c.UserAppTokens));
        }

        public override ClientApp Get(Guid key)
        {
            return All().SingleOrDefault(bu => bu.Id == key).As<ClientApp>();
        }

        internal override IEnumerable<String> Includes
        {
            get { return new List<string>(); }
        }

        #endregion

        #region IClientAppRepository

        public ClientApp GetClientAppByName(string clientAppName)
        {
            return base.AllMatching(c => c.Name == clientAppName).SingleOrDefault().As<ClientApp>();
        }

        #endregion
    }
}
