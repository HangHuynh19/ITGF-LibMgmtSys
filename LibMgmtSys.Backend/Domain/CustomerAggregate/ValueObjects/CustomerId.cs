using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects
{
    public class CustomerId : ValueObject
    {
        public Guid Value { get; private set; }

        private CustomerId(Guid value)
        {
            Value = value;
        }

        public static CustomerId CreateUnique()
        {
            return new CustomerId(Guid.NewGuid());
        }

        public static CustomerId Create(Guid value)
        {
            return new CustomerId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}