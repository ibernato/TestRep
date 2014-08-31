using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public class UserAppTokenModel
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Nullable<System.DateTime> TokenExpiration { get; set; }

        public Guid AppUser_Id { get; set; }
        public UserModel User { get; set; }
        
        public Guid ClientAppId { get; set; }
        public ClientAppModel ClientApp { get; set; }
    }
}
