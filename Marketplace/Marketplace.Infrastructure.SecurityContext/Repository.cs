namespace Marketplace.Infrastructure.Repository
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using Marketplace.Core;
    using Marketplace.Core.Logging;
    using Marketplace.Infrastructure.SecurityContext.UnitOfWork;
    using System.Data.Entity.Infrastructure;
    using Marketplace.Infrastructure;

    public abstract class Repository<TEntity, TModelEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity
        where TModelEntity : class, new()
    {
        #region Members

        SecurityUnitOfWork _UnitOfWork;

        #endregion

        #region Constructor

        public Repository(SecurityUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        internal void Create(TModelEntity item)
        {
            if (item != (TModelEntity)null)
            {
                item.ApplyCreatedAudit();
                item.ApplyVersion();
                item.ChangeObjectState(State.Active);

                _UnitOfWork.CreateSet<TModelEntity>().Add(item);
            }
        }

        public virtual void Create(TEntity item)
        {
            var modelItem = item.As<TModelEntity>();
            this.Create(modelItem);
        }

        public virtual void Delete(TKey key)
        {
            var item = _UnitOfWork.CreateSet<TModelEntity>().Find(key);

            if (item != (TEntity)null)
            {
                item.ApplyModifiedAudit();
                item.ApplyVersion();

                _UnitOfWork.Attach(item);

                if (item is IObjectState)
                {
                    item.ChangeObjectState(State.Active);

                    _UnitOfWork.SetModified(item.As<TModelEntity>());
                }
                else
                {
                    _UnitOfWork.CreateSet<TModelEntity>().Remove(item);
                }
            }
        }

        public virtual void Update(TEntity item)
        {
            if (item != (TEntity)null)
            {
                _UnitOfWork.SetModified(item.As<TModelEntity>());
            }
        }

        internal void Update(TModelEntity item)
        {
            if (item != (TModelEntity)null)
            {
                item.ApplyModifiedAudit();
                item.ApplyVersion();

                _UnitOfWork.SetModified(item);
            }
        }

        public abstract TEntity Get(TKey key);

        internal IEnumerable<TModelEntity> AllMatching(Expression<Func<TModelEntity, bool>> query)
        {
            return All().Where(query).ToList();
        }

        internal DbQuery<TModelEntity> All()
        {
            return _UnitOfWork.CreateSet<TModelEntity>().Includes(Includes.ToArray());
        }

        internal abstract IEnumerable<String> Includes { get; }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }

        #endregion
    }

}
