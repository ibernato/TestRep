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
    [Export(typeof(RepositoryFactory<IBusinessUnitRepository>))]
    public class BusinessUnitRepositoryFactory : RepositoryFactory<IBusinessUnitRepository>
    {
        public override IBusinessUnitRepository Create(IUnitOfWork unitOfWork)
        {
            if (!(unitOfWork is SecurityUnitOfWork))
                throw new Exception("SecurityUnitOfWork is expected.");

            return new BusinessUnitRepository((SecurityUnitOfWork)unitOfWork);
        }
    }
}
