using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.Common.ValueObjects
{
    public sealed class Rating : ValueObject
    {
        public Rating(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public static Rating Create(int value)
        {
            return new Rating(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}