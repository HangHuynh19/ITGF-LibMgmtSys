using LibMgmtSys.Backend.Contracts.Books;
using LibMgmtSys.Backend.Domain.BookAggregate;

namespace LibMgmtSys.Backend.Contracts.Authors
{
  public record AuthorResponse(
    string Id,
    string Name,
    string Biography,
    List<string> BookNames
  );
}
