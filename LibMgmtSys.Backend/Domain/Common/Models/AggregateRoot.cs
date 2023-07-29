using LibMgmtSys.Backend.Domain.Common.Models.Identities;

namespace LibMgmtSys.Backend.Domain.Common.Models
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }
    }
}