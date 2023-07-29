using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects
{
    public sealed class GenreId : ValueObject
    {
        public Guid Value { get; private set; }
        public GenreId(Guid value)
        {
            Value = value;
        }

        public static GenreId CreateUnique()
        {
            return new GenreId(Guid.NewGuid());
        }

        public static GenreId Create(Guid value)
        {
            return new GenreId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}