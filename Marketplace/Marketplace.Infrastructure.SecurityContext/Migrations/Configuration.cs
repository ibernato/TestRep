using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Infrastructure.SecurityContext.UnitOfWork;
using Marketplace.Security;

namespace Marketplace.Infrastructure.SecurityContext.Migrations
{

    public sealed class Configuration : DbMigrationsConfiguration<SecurityUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SecurityUnitOfWork context)
        {
            //NOTE: Each time you change the content of this method, ALL the records will be added to the database!!
            //If you change this method, it is better to delete de whole database first.

            ////Demo data

            //Users
            //var user = new User();
            //user.Username = "test";
            //user.LCID = 1033;            
            //user.GenerateNewIdentity();
            //context.Users.Add(user);
        }
    }
}
