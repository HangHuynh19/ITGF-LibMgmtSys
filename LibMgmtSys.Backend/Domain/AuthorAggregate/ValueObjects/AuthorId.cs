using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects
{
    public sealed class AuthorId : ValueObject
    {
        public Guid Value { get; private set; }
        public AuthorId(Guid value)
        {
            Value = value;
        }

        public static AuthorId CreateUnique()
        {
            return new AuthorId(Guid.NewGuid());
        }

        public static AuthorId Create(Guid value)
        {
            return new AuthorId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}