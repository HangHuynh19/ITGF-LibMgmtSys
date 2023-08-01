using LibMgmtSys.Backend.Contracts.Authors;

namespace LibMgmtSys.Backend.Contracts.Books
{
  public record BookResponse(
    string Id,
    string Title,
    List<AuthorResponse> Authors,
    string Isbn,
    string Publisher,
    int Year,
    //List<GenreResponse> Genres,
    string Description,
    float? AverageRating,
    Uri Image,
    int BorrowingPeriod,
    int Quantity,
    //List<BookReviewResponse> BookReviews,
    DateTime CreatedAt,
    DateTime UpdatedAt
  );
}