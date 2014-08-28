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
    [Export(typeof(IUserGroupRepository))]
    public class UserGroupRepository : Repository<UserGroup, Marketplace.Infrastructure.SecurityContext.Entities.UserGroupModel, Guid>, IUserGroupRepository
    {
        #region Constructor

        [ImportingConstructor]
        public UserGroupRepository(SecurityUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        public IEnumerable<UserGroup> FindAllUserGroupsWithId(Guid[] guid)
        {
            return base.AllMatching(ug => guid.Contains(ug.Id)).AsCollection<UserGroup>();
        }

        public override UserGroup Get(Guid key)
        {
            return All().SingleOrDefault(bu => bu.Id == key).As<UserGroup>();
        }

        internal override IEnumerable<string> Includes
        {
            get { return new List<string>() { }; }
        }
    }

}
