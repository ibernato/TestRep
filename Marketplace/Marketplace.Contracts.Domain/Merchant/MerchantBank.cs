using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Contracts.Domain.Merchant
{
    public class MerchantBank
    {
        public virtual String Bank { get; set; }

        public virtual String Account { get; set; }
    }
}
