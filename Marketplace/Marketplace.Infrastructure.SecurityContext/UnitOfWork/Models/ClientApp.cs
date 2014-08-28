using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public partial class ClientAppModel
    {
        #region Constructor

        public ClientAppModel()
        {
            UserAppTokens = new HashSet<UserAppTokenModel>();
        }

        #endregion

        #region Primitive Properties

        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string ClientToken { get; set; }

        public virtual int TokenValidity { get; set; }

        public virtual int PersistentTokenValidity { get; set; }

        public virtual int RequestTimeOffset { get; set; }

        #endregion

        #region Complex Properties

        public virtual Auditing AuditInfo { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<UserAppTokenModel> UserAppTokens { get; set; }

        #endregion
    }
}
