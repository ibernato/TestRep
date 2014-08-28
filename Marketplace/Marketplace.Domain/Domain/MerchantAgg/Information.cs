using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Domain.MerchantAgg
{
    public class Information : ValueObject<Information>
    {
        #region Properties 

        public virtual String Note { get; set; }

        public virtual String Analysis { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual String CompanyDirectorTitle { get; set; }

        public virtual Decimal DeliveryFee { get; set; }

        #endregion
    }
}
