using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Domain.MerchantAgg
{
    public class Location : ValueObject<Location>
    {
        #region Properties

        public virtual int Id { get; set; }

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
