using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Security.Contracts
{
    public interface IUserRepository : IRepository<User, Guid>
    {

        User GetUserByUsername(string userName);
    }
}
