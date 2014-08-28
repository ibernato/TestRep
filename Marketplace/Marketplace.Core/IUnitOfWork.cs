namespace Marketplace.Core
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
