using LibMgmtSys.Backend.Contracts.Authors;
using LibMgmtSys.Backend.Domain.AuthorAggregate;

namespace LibMgmtSys.Backend.Contracts.Books
{
  public record BookResponse(
    string Id,
    string Title,
    List<string> AuthorNames,
    string Isbn,
    string Publisher,
    int Year,
    //List<GenreResponse> Genres,
    string Description,
    float? AverageRating,
    Uri Image,
    int BorrowingPeriod,
    int Quantity
    //List<BookReviewResponse> BookReviews,
  );
}