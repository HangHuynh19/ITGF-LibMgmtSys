using LibMgmtSys.Backend.Domain.GenreAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Genres.Queries
{
    public record GetAllGenresQuery() : IRequest<ErrorOr<List<Genre>>>;
}

