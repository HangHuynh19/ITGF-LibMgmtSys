namespace LibMgmtSys.Backend.Domain.Common.Models.Identities
{
    public sealed class Rating : ValueObject
    {
        public Rating(int value)
        {
            Value = value;
        }

        public int Value { get; }

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