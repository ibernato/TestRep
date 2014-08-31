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
using Marketplace.Infrastructure.SecurityContext.Entities;
using System.Data.Entity;
using RefactorThis.GraphDiff;

namespace Marketplace.Infrastructure.SecurityContext.Repositories
{
    [Export(typeof(IUserGroupRepository))]
    public class UserGroupRepository : Repository<UserGroup, UserGroupModel, Guid>, IUserGroupRepository
    {
        #region Constructor

        [ImportingConstructor]
        public UserGroupRepository(SecurityUnitOfWork unitOfWork)
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

        public override UserGroup Get(Guid key)
        {
            var item = (base.UnitOfWork as DbContext).Set<UserGroupModel>().Find(key);
            return item.As<UserGroup>();
        }

        public override void Update(UserGroup item)
        {
            Context.UpdateGraph<UserGroupModel>(item.As<UserGroupModel>().ApplyVersion().ApplyModifiedAudit().ChangeObjectState(),  
                u => u.AssociatedCollection(p => p.Users).OwnedCollection(p => p.Permissions));
        }

        internal override IEnumerable<string> Includes
        {
            get { return new List<string>() { "Users", "Permissions" }; }
        }

        #endregion

        #region IUserGroupRepository

        public IEnumerable<UserGroup> FindAllUserGroupsWithId(Guid[] guid)
        {
            return base.AllMatching(ug => guid.Contains(ug.Id)).AsCollection<UserGroup>();
        }

        #endregion
    }

}
