using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Core;
using Marketplace.Infrastructure.SecurityContext.Repositories;
using Marketplace.Infrastructure.SecurityContext.UnitOfWork;
using Marketplace.Security.Contracts;

namespace Marketplace.Infrastructure
{
    [Export(typeof(RepositoryFactory<IClientAppRepository>))]
    public class ClientAppRepositoryFactory : RepositoryFactory<IClientAppRepository>
    {
        public override IClientAppRepository Create(IUnitOfWork unitOfWork)
        {
            if (!(unitOfWork is SecurityUnitOfWork))
                throw new Exception("SecurityUnitOfWork is expected.");

            return new ClientAppRepository((SecurityUnitOfWork)unitOfWork);
        }
    }
}
