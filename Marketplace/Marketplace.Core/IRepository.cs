namespace Marketplace.Core
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<TEntity, in TKey> : IDisposable, IRepository where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        void Create(TEntity item);

        void Delete(TKey key);

        void Update(TEntity item);

        TEntity Get(TKey key);
    }

    public interface IRepository
    {

    }
}
