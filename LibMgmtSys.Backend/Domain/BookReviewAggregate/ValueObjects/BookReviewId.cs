using LibMgmtSys.Backend.Domain.Common.Models;

namespace LibMgmtSys.Backend.Domain.BookReviewAggregate.ValueObjects
{
    public class BookReviewId : ValueObject
    {
        public Guid Value { get; }

        private BookReviewId(Guid value)
        {
            Value = value;
        }

        public static BookReviewId CreateUnique()
        {
            return new BookReviewId(Guid.NewGuid());
        }

        public static BookReviewId Create(Guid bookReviewId)
        {
            return new BookReviewId(bookReviewId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}