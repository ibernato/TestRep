using System;
using System.Data.Entity;

namespace Marketplace.Infrastructure
{
    public static class AuditingExtensions
    {
        public static T ApplyCreatedAudit<T>(this T entity) where T : class
        {
            if (entity is ICreatedAuditing)
            {
                ((ICreatedAuditing)entity).Created = new AuditingInfo();

                ((ICreatedAuditing)entity).Created.By = Guid.NewGuid();
                ((ICreatedAuditing)entity).Created.On = DateTime.UtcNow;
            }

            if (entity is IModifiedAuditing)
            {
                ((IModifiedAuditing)entity).Modified = new AuditingInfo();
            }

            return entity;
        }

        public static T ApplyModifiedAudit<T>(this T entity) where T : class
        {
            if (entity is ICreatedAuditing)
            {
                ((ICreatedAuditing)entity).Created = new AuditingInfo();
            }

            if (entity is IModifiedAuditing)
            {
                ((IModifiedAuditing)entity).Modified = new AuditingInfo();

                ((IModifiedAuditing)entity).Modified.By = Guid.NewGuid();
                ((IModifiedAuditing)entity).Modified.On = DateTime.UtcNow;
            }

            return entity;
        }
    }
}
