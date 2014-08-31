using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marketplace.Core;

namespace Marketplace.Infrastructure.SecurityContext.Entities
{
    public class ContactModel
    {
        public Guid Id { get; set; }
        public string Tel { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserModel User { get; set; }
    }
}
