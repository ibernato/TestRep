using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;
using Marketplace.Validation;

namespace Marketplace.Domain.MerchantAgg
{
    public class Merchant : AggregateRoot, IValidateObject
    {
        #region Properties

        public virtual String Title { get; set; }

        public virtual String VisibleTitle { get; set; }

        public virtual String OIB { get; set; }

        public virtual String Mail { get; set; }

        public virtual String Account { get; set; }



        public virtual Boolean IsConfirmed { get; set; }

        public virtual Boolean IsActive { get; set; }

        public virtual Boolean IsContractSigned { get; set; }

        public virtual Boolean IsOnImperios { get; set; }


        // Image
        public virtual Guid Logo { get; set; }


        public virtual Information Information { get; set; }

        public virtual List<Location> Locations { get; set; }

        #endregion

        #region Public Methods

        public void SetBilling(Int32 locationId)
        {
            if (Locations == null)
                throw new NullReferenceException("Locations");

            if (Locations.Count == 0)
                throw new IndexOutOfRangeException("Locations");

            Locations.ForEach(l => l.IsBilling = false);
            Locations.Single(l => l.Id == locationId).IsBilling = true;
        }

        public void SetLogo(Guid logoGuid)
        {
            if (logoGuid == Guid.Empty)
                throw new ArgumentException("logoGuid");

            Logo = logoGuid;
        }

        public void AddLocation(Location location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            Locations.Add(location);
        }

        #endregion

        public static Merchant Create()
        {
            throw new NotImplementedException();
        }

        #region Validation

        public void Validate()
        {
            if (string.IsNullOrEmpty(OIB))
                throw new ArgumentNullException("OIB");

            if (string.IsNullOrEmpty(Account))
                throw new ArgumentNullException("Account");
        }

        #endregion
    }
}
