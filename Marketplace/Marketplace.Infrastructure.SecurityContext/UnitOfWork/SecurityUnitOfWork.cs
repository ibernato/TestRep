using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.SecurityContext.UnitOfWork
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using Marketplace.Core;
    using Marketplace.Infrastructure.SecurityContext.Entities;
    using Marketplace.Infrastructure.SecurityContext.Migrations;
    using Marketplace.Infrastructure.SecurityContext.UnitOfWork.Mapping;

    [Export("SecurityContext", typeof(IUnitOfWork))]
    public class SecurityUnitOfWork : DbContext, IUnitOfWork
    {
        #region IDbSet Members

        IDbSet<UserModel> _users;
        public IDbSet<UserModel> Users
        {
            get
            {
                if (_users == null)
                    _users = base.Set<UserModel>();

                return _users;
            }
        }

        IDbSet<BusinessUnitModel> _businessUnits;
        public IDbSet<BusinessUnitModel> BusinessUnits
        {
            get
            {
                if (_businessUnits == null)
                    _businessUnits = base.Set<BusinessUnitModel>();

                return _businessUnits;
            }
        }

        IDbSet<ClientAppModel> _clientApps;
        public IDbSet<ClientAppModel> ClientApps
        {
            get
            {
                if (_clientApps == null)
                    _clientApps = base.Set<ClientAppModel>();

                return _clientApps;
            }
        }

        IDbSet<ContactModel> _contacts;
        public IDbSet<ContactModel> Contacts
        {
            get
            {
                if (_contacts == null)
                    _contacts = base.Set<ContactModel>();

                return _contacts;
            }
        }

        IDbSet<PermissionModel> _permissions;
        public IDbSet<PermissionModel> Permissions
        {
            get
            {
                if (_permissions == null)
                    _permissions = base.Set<PermissionModel>();

                return _permissions;
            }
        }

        IDbSet<UserAppTokenModel> _userAppTokens;
        public IDbSet<UserAppTokenModel> UserAppTokens
        {
            get
            {
                if (_userAppTokens == null)
                    _userAppTokens = base.Set<UserAppTokenModel>();

                return _userAppTokens;
            }
        }

        IDbSet<UserGroupModel> _userGroups;
        public IDbSet<UserGroupModel> UserGroups
        {
            get
            {
                if (_userGroups == null)
                    _userGroups = base.Set<UserGroupModel>();

                return _userGroups;
            }
        }

        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item)
            where TEntity : class
        {
            //this operation also attach item in object state manager
            base.Entry<TEntity>(item).State = EntityState.Modified;
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void Rollback()
        {
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion

        public SecurityUnitOfWork()
            : base("name=SecurityModelConnectionString")
        {
            Database.SetInitializer<SecurityUnitOfWork>(null);

            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<AuditingInfo>();
            modelBuilder.ComplexType<Versioning>();
            modelBuilder.ComplexType<ObjectState>();

            //Remove unused conventions
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Add(new PluralizingTableNameConvention());
            modelBuilder.Conventions.Add(new PluralizingEntitySetNameConvention());

            //Add entity configurations in a structured way using 'TypeConfiguration’ classes
            modelBuilder.Configurations.Add(new ClientAppEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new BusinessUnitEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ContactEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PermissionEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserAppTokenEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserGroupEntityTypeConfiguration());
        }

        #endregion
    }
}

/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.SecurityContext.UnitOfWork
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using Marketplace.Core;
    using Marketplace.Infrastructure.SecurityContext.Entities;
    using Marketplace.Infrastructure.SecurityContext.Migrations;
    using Marketplace.Infrastructure.SecurityContext.UnitOfWork.Mapping;

    [Export(typeof(SecurityUnitOfWork))]
    public class SecurityUnitOfWork : DbContext, IUnitOfWork
    {
        #region IDbSet Members

        IDbSet<User> _users;
        public IDbSet<User> Users
        {
            get
            {
                if (_users == null)
                    _users = base.Set<User>();

                return _users;
            }
        }

        IDbSet<BusinessUnit> _businessUnits;
        public IDbSet<BusinessUnit> BusinessUnits
        {
            get
            {
                if (_businessUnits == null)
                    _businessUnits = base.Set<BusinessUnit>();

                return _businessUnits;
            }
        }

        IDbSet<ClientApp> _clientApps;
        public IDbSet<ClientApp> ClientApps
        {
            get
            {
                if (_clientApps == null)
                    _clientApps = base.Set<ClientApp>();

                return _clientApps;
            }
        }

        IDbSet<Contact> _contacts;
        public IDbSet<Contact> Contacts
        {
            get
            {
                if (_contacts == null)
                    _contacts = base.Set<Contact>();

                return _contacts;
            }
        }

        IDbSet<Permission> _permissions;
        public IDbSet<Permission> Permissions
        {
            get
            {
                if (_permissions == null)
                    _permissions = base.Set<Permission>();

                return _permissions;
            }
        }

        IDbSet<UserAppToken> _userAppTokens;
        public IDbSet<UserAppToken> UserAppTokens
        {
            get
            {
                if (_userAppTokens == null)
                    _userAppTokens = base.Set<UserAppToken>();

                return _userAppTokens;
            }
        }

        IDbSet<UserGroup> _userGroups;
        public IDbSet<UserGroup> UserGroups
        {
            get
            {
                if (_userGroups == null)
                    _userGroups = base.Set<UserGroup>();

                return _userGroups;
            }
        }

        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet CreateSet(Type entityType)
        {
            return base.Set(entityType);
        }

        public void Attach(object item)
        {
            //attach and set as unchanged
            base.Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified(object item)
        {
            //this operation also attach item in object state manager
            base.Entry(item).State = EntityState.Modified;
        }

        public void SetDeleted(object item)
        {
            //this operation also attach item in object state manager
            base.Entry(item).State = EntityState.Deleted;
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void Rollback()
        {
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion

        public SecurityUnitOfWork()
            : base("name=SecurityModelConnectionString")
        {
            Database.SetInitializer<SecurityUnitOfWork>(null);
        }

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<Auditing>();

            //Remove unused conventions
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Add(new PluralizingTableNameConvention());
            modelBuilder.Conventions.Add(new PluralizingEntitySetNameConvention());

            //Add entity configurations in a structured way using 'TypeConfiguration’ classes
            modelBuilder.Configurations.Add(new ClientAppEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new BusinessUnitEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ContactEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PermissionEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserAppTokenEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new UserGroupEntityTypeConfiguration());
        }

        #endregion
    }
}

 * 
*/