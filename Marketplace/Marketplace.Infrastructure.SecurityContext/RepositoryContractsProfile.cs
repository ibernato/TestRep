using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Infrastructure.SecurityContext.Entities;
using Marketplace.Security;

namespace Marketplace.Infrastructure.SecurityContext
{
    [Export(typeof(Profile))]
    public class RepositoryContractsProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<User, UserModel>();
            this.CreateMap<BusinessUnit, BusinessUnitModel>();
            this.CreateMap<ClientApp, ClientAppModel>();
            this.CreateMap<Permission, PermissionModel>();
            this.CreateMap<UserAppToken, UserAppTokenModel>();
            this.CreateMap<UserGroup, UserGroupModel>();
            this.CreateMap<Contact, ContactModel>();

            this.CreateMap<UserModel, User>();
            this.CreateMap<BusinessUnitModel, BusinessUnit>();
            this.CreateMap<ClientAppModel, ClientApp>();
            this.CreateMap<PermissionModel, Permission>();
            this.CreateMap<UserAppTokenModel, UserAppToken>();
            this.CreateMap<UserGroupModel, UserGroup>();
            this.CreateMap<ContactModel, Contact>();
        }

        public override string ProfileName
        {
            get
            {
                return "RepositoryContracts";
            }
        }
    }
}
