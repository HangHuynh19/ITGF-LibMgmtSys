using LibMgmtSys.Backend.Domain.AuthorAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Authors.Queries
{
    public record GetAllAuthorsQuery() : IRequest<ErrorOr<List<Author>>>;
}
