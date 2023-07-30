using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects
{
    public class CustomerId : ValueObject
    {
        public Guid Value { get; }

        private CustomerId(Guid value)
        {
            Value = value;
        }

        public static CustomerId CreateUnique()
        {
            return new CustomerId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}