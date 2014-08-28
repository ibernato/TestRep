using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Security.Contracts
{
    public interface IUserGroupRepository : IRepository<UserGroup, Guid>
    {

        IEnumerable<UserGroup> FindAllUserGroupsWithId(Guid[] guid);
    }
}
