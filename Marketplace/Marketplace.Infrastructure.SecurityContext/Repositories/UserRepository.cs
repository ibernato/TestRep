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
using RefactorThis.GraphDiff;
using Marketplace.Infrastructure.SecurityContext.Entities;
using System.Data.Entity;

namespace Marketplace.Infrastructure.SecurityContext.Repositories
{
    [Export(typeof(IUserRepository))]
    public class UserRepository : Repository<User, UserModel, Guid>, IUserRepository
    {
        #region Constructor

        [ImportingConstructor]
        public UserRepository(SecurityUnitOfWork unitOfWork)
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

        public override void Update(User item)
        {
            var model = Context.UpdateGraph<UserModel>(item.As<UserModel>().ApplyVersion().ApplyModifiedAudit().ChangeObjectState(),
                u => u.AssociatedCollection(p => p.UserGroups).OwnedCollection(p => p.UserAppTokens).OwnedEntity(p => p.Contact));

            Context.Entry(model).Property("Created").IsModified = false;
        }

        public override User Get(Guid key)
        {
            return All().SingleOrDefault(bu => bu.Id == key).As<User>();
        }
        
        internal override IEnumerable<string> Includes
        {
            get { return new List<string>() { "UserAppTokens", "Contact" }; }
        }

        #endregion

        #region IUserRepository

        public User GetUserByUsername(string userName)
        {
            var user = base.AllMatching(u => u.Username == userName).SingleOrDefault();
            return user.As<User>();
        }

        #endregion
    }
}
