using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Core;

namespace Marketplace.Core
{
    public abstract class RepositoryFactory<T>
    {
        public abstract T Create(IUnitOfWork unitOfWork);
    }
}
