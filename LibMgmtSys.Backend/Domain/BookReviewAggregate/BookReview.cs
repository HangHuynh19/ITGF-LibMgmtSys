using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookReviewAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.Common.ValueObjects;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.BookReviewAggregate
{
    public class BookReview : AggregateRoot<BookReviewId>
    {
        public string Comment { get; private set; }
        public Rating Rating { get; private set; }
        public BookId BookId { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Book Book { get; private set; }
        public Customer Customer { get; private set; }
        private BookReview() : base(BookReviewId.CreateUnique())
        {
        }
        
        private BookReview(
          BookReviewId bookReviewId,
          string comment,
          Rating rating,
          BookId bookId,
          CustomerId customerId
          ) : base(bookReviewId)
        {
            Comment = comment;
            Rating = rating;
            BookId = bookId;
            CustomerId = customerId;
        }

        public static BookReview Create(
          string comment,
          int rating,
          BookId bookId,
          CustomerId customerId,
          BookReviewId? bookReviewId = null
          )
        {
            var ratingValueObject = Rating.Create(rating);

            return new BookReview(
              bookReviewId ?? BookReviewId.CreateUnique(),
              comment,
              ratingValueObject,
              bookId,
              customerId
              );
        }
    }
}