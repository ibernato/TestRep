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
    [Export(typeof(IClientAppRepository))]
    public class ClientAppRepository : Repository<ClientApp, Marketplace.Infrastructure.SecurityContext.Entities.ClientAppModel, Guid>, IClientAppRepository
    {
        #region Constructor

        [ImportingConstructor]
        public ClientAppRepository(SecurityUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion

        public ClientApp GetClientAppByName(string clientAppName)
        {
            return base.AllMatching(c => c.Name == clientAppName).SingleOrDefault().As<ClientApp>();
        }

        public override ClientApp Get(Guid key)
        {
            return All().SingleOrDefault(bu => bu.Id == key).As<ClientApp>();
        }

        internal override IEnumerable<String> Includes
        {
            get { return new List<string>(); }
        }
    }
}
