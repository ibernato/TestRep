using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Contracts.Domain.Merchant
{
    public class Merchant
    {
        public virtual int Id { get; set; }


        public virtual String Title { get; set; }

        public virtual String VisibleTitle { get; set; }

        public virtual String CompanyDirectorTitle { get; set; }

        public virtual String OIB { get; set; }

        public virtual String MBR { get; set; }

        public virtual 

        // Image
        public virtual Guid Logo { get; set; }



        public virtual MerchantBank Bank { get; set; }
    }
}
