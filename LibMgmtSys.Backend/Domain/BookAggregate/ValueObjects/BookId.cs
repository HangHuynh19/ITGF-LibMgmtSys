using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects
{
    public sealed class BookId : ValueObject
    {
        public Guid Value { get; private set; }
        public BookId(Guid value)
        {
            Value = value;
        }

        public static BookId CreateUnique()
        {
            return new BookId(Guid.NewGuid());
        }

        public static BookId Create(Guid value)
        {
            return new BookId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}