using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public class UserModel : ICreatedAuditing, IModifiedAuditing, IObjectState, IVersioning
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int LCID { get; set; }

        
        public Nullable<Guid> BusinessUnitId { get; set; }
        public BusinessUnitModel BusinessUnit { get; set; }

        public ContactModel Contact { get; set; }
        public ICollection<UserGroupModel> UserGroups { get; set; }
        public ICollection<UserAppTokenModel> UserAppTokens { get; set; }


        public AuditingInfo Created { get; set; }
        public AuditingInfo Modified { get; set; }
        public ObjectState ObjectState { get; set; }
        public Versioning Version { get; set; }
    }
}
