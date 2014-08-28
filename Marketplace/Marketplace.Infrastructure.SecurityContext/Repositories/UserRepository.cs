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

        public override void Create(User item)
        {
            var modelItem = item.As<UserModel>();

            modelItem.Contact.Id = modelItem.Id;

            base.Create(modelItem);
        }

        public override void Update(User item)
        {
            var modelItem = GetModel(item.Id);
            base.Update(item.As<User, UserModel>(modelItem));
        }

        public User GetUserByUsername(string userName)
        {
            var user = base.AllMatching(u => u.Username == userName).SingleOrDefault();
            return user.As<User>();
        }

        public override User Get(Guid key)
        {
            return All().SingleOrDefault(bu => bu.Id == key).As<User>();
        }
        
        internal UserModel GetModel(Guid key)
        {
            return All().SingleOrDefault(bu => bu.Id == key);
        }

        internal override IEnumerable<string> Includes
        {
            get { return new List<string>() { "UserAppTokens", "Contact" }; }
        }
    }
}
