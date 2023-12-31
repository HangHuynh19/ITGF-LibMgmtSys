using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookReviewAggregate;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.Common.ValueObjects;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate;

namespace LibMgmtSys.Backend.Domain.BookAggregate
{
  public class Book : AggregateRoot<BookId>
  {
    private readonly List<Author> _authors = new();
    private readonly List<Genre> _genres = new();
    private readonly List<BookReview> _bookReviews = new();
    private readonly List<Loan> _loans = new();

    public string Title { get; private set; }
    public IReadOnlyList<Author> Authors => _authors.AsReadOnly();
    public string Isbn { get; private set; }
    public string Publisher { get; private set; }
    public int Year { get; private set; }
    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();
    public string Description { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public Uri? Image { get; private set; } = new Uri("https://i.imgur.com/1qk9n0z.png");
    public int BorrowingPeriod { get; private set; }
    public int Quantity { get; private set; }
    public IReadOnlyList<BookReview> BookReviews => _bookReviews.AsReadOnly();
    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

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
      AverageRating averageRating,
      int borrowingPeriod,
      int quantity,
      Uri? image = null
      ) : base(bookId)
    {
      Title = title;
      Isbn = isbn;
      Publisher = publisher;
      Year = year;
      Description = description;
      AverageRating = averageRating;
      BorrowingPeriod = borrowingPeriod;
      Quantity = quantity;
      Image = image ?? new Uri("https://i.imgur.com/1qk9n0z.png");
    }

    public static Book Create(
      string title,
      string isbn,
      string publisher,
      int year,
      string description,
      int borrowingPeriod,
      int quantity,
      Uri? image = null)
    {
      return new Book(
        BookId.CreateUnique(),
        title,
        isbn,
        publisher,
        year,
        description,
        AverageRating.CreateNew(),
        borrowingPeriod,
        quantity,
        image);
    }

    public void AddAuthor(Author author)
    {
      _authors.Add(author);
    }

    public void RemoveAuthor(Author author)
    {
      _authors.Remove(author);
    }

    public void AddGenre(Genre genre)
    {
      _genres.Add(genre);
    }

    public void RemoveGenre(Genre genre)
    {
      _genres.Remove(genre);
    }

    public void UpdateBookProperties(
      string title,
      string isbn,
      string publisher,
      int year,
      string description,
      Uri image,
      int borrowingPeriod,
      int quantity)
    {
      Title = title;
      Isbn = isbn;
      Publisher = publisher;
      Year = year;
      Description = description;
      Image = image;
      BorrowingPeriod = borrowingPeriod;
      Quantity = quantity;
    }

    public void ClearAuthors()
    {
      _authors.Clear();
    }

    public void ClearGenres()
    {
      _genres.Clear();
    }
  }
}