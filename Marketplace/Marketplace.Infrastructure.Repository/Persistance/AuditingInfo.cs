using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Infrastructure
{
    public class AuditingInfo
    {
        public Nullable<System.DateTime> On { get; set; }

        public Nullable<Guid> By { get; set; }
    }
}
