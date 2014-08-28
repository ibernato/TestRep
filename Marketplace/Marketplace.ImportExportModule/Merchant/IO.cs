using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.ImportExportModule.Merchant
{
    public class IO
    {
        public virtual Boolean IsUpdated { get; set; }

        public virtual Int32 ImportCount { get; set; }

        public virtual String XmlUrl { get; set; }

        public virtual String XmlApiKey { get; set; }
    }
}
