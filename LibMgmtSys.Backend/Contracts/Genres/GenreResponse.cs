using LibMgmtSys.Backend.Domain.BookAggregate;

namespace Contracts.Genres
{
    public record GenreResponse(
        string Id,
        string Name,
        List<string> BookNames
    );
}