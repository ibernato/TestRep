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
    public class ClientAppModel : ICreatedAuditing, IModifiedAuditing, IVersioning, IObjectState
    {
        public ClientAppModel()
        {
            UserAppTokens = new HashSet<UserAppTokenModel>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ClientToken { get; set; }
        public int TokenValidity { get; set; }
        public int PersistentTokenValidity { get; set; }
        public int RequestTimeOffset { get; set; }
        
        public ICollection<UserAppTokenModel> UserAppTokens { get; set; }


        public AuditingInfo Created { get; set; }
        public AuditingInfo Modified { get; set; }
        public ObjectState ObjectState { get; set; }
        public Versioning Version { get; set; }
    }
}
