namespace LibMgmtSys.Backend.Contracts.Books
{
  public record CreateBookRequest(
    string Title,
    string Isbn,
    string Publisher,
    List<string> AuthorIds,
    int Year,
    List<string> GenreIds,
    string Description,
    Uri? Image,
    int BorrowingPeriod,
    int Quantity
  //List<BookReviewId> BookReviewIds
  );
}