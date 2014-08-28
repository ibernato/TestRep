using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Contracts.Domain.Merchant
{
    public class MerchantLocation
    {
        #region Properties

        public virtual string Title { get; set; }

        public virtual string Country { get; set; }

        public virtual string City { get; set; }

        public virtual string ZipNo { get; set; }

        public virtual string Tel { get; set; }

        public virtual string Address { get; set; }

        public virtual bool IsMain { get; set; }

        public virtual bool IsBilling { get; set; }

        #endregion

    }
}
