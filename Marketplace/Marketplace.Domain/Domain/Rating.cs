using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Domain.Merchant
{
    public class Rating : ValueObject<Rating>
    {
        #region Properties

        public virtual Int32 Year { get; set; }

        public virtual Int32 Month { get; set; }

        public virtual Decimal Value { get; set; }

        public virtual DateTime AverageConfirmTime { get; set; }

        #endregion
    }
}
