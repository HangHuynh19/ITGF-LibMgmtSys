using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Genres
{
    public record CreateGenreCommand(
        string Name,
        List<BookId> BookIds
        ) : IRequest<ErrorOr<Genre>>;
}

