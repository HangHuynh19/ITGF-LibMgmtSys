using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookReviewAggregate;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.Common.ValueObjects;
using LibMgmtSys.Backend.Domain.GenreAggregate;

namespace LibMgmtSys.Backend.Domain.BookAggregate
{
  public class Book : AggregateRoot<BookId>
  {
    private readonly List<Author> _authors = new();
    private readonly List<Genre> _genres = new();
    private readonly List<BookReview> _bookReviews = new();

    public string Title { get; private set; }
    public IReadOnlyList<Author> Authors => _authors.AsReadOnly();
    public string Isbn { get; private set; }
    public string Publisher { get; private set; }
    public int Year { get; private set; }
    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();
    public string Description { get; private set; }
   //public AverageRating AverageRating { get; private set; }
    public Uri Image { get; private set; }
    public int BorrowingPeriod { get; private set; }
    public int Quantity { get; private set; }
    public IReadOnlyList<BookReview> BookReviews => _bookReviews.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Book() : base(BookId.CreateUnique())
    {
    }

    private Book(
      BookId bookId,
      string title,
      string isbn,
      string publisher,
      int year,
      string description,
      //AverageRating averageRating,
      Uri image,
      int borrowingPeriod,
      int quantity
      ) : base(bookId)
    {
      Title = title;
      Isbn = isbn;
      Publisher = publisher;
      Year = year;
      Description = description;
      //AverageRating = averageRating;
      Image = image;
      BorrowingPeriod = borrowingPeriod;
      Quantity = quantity;
    }

    public static Book Create(
      string title,
      string isbn,
      string publisher,
      int year,
      string description,
      Uri image,
      int borrowingPeriod,
      int quantity)
    {
      return new Book(
        BookId.CreateUnique(),
        title,
        isbn,
        publisher,
        year,
        description,
        //AverageRating.CreateNew(),
        image,
        borrowingPeriod,
        quantity);
    }
  }
}