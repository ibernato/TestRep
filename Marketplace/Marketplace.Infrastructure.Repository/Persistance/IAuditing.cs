using System;

namespace Marketplace.Infrastructure
{
    public interface ICreatedAuditing
    {
        AuditingInfo Created { get; set; }
    }

    public interface IModifiedAuditing
    {
        AuditingInfo Modified { get; set; }
    }
}
