using LibMgmtSys.Backend.Contracts.Books;

namespace LibMgmtSys.Backend.Contracts.Authors
{
  public record AuthorResponse(
    string Id,
    string Name,
    string Biography,
    List<BookResponse> Books
  );
}
