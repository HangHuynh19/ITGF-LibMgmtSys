using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookReviewAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.Common.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Domain.BookReviewAggregate
{
    public class BookReview : AggregateRoot<BookReviewId>
    {
        public string Comment { get; private set; }
        public Rating Rating { get; private set; }
        public BookId BookId { get; private set; }
        public UserId UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public Book Book { get; private set; }
        private BookReview() : base(BookReviewId.CreateUnique())
        {
        }
        
        private BookReview(
          BookReviewId bookReviewId,
          string comment,
          Rating rating,
          BookId bookId,
          UserId userId
          ) : base(bookReviewId)
        {
            Comment = comment;
            Rating = rating;
            BookId = bookId;
            UserId = userId;
        }

        public static BookReview Create(
          string comment,
          int rating,
          BookId bookId,
          UserId userId,
          BookReviewId? bookReviewId = null
          )
        {
            var ratingValueObject = Rating.Create(rating);

            return new BookReview(
              bookReviewId ?? BookReviewId.CreateUnique(),
              comment,
              ratingValueObject,
              bookId,
              userId
              );
        }
    }
}