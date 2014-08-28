using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Core.Logging;

namespace Marketplace.Security
{
    public partial class ClientApp : AggregateRoot
    {
        #region Constructor

        public ClientApp()
            : base()
        {

        }

        #endregion

        #region Primitive Properties

        public virtual string Name { get; set; }

        public virtual string ClientToken { get; set; }

        public virtual int TokenValidity { get; set; }

        public virtual int PersistentTokenValidity { get; set; }

        public virtual int RequestTimeOffset { get; set; }

        #endregion

    }
}
