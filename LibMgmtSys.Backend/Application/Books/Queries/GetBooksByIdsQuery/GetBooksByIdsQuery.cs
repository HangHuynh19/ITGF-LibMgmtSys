using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetBooksByIdsQuery
{
    public record GetBooksByIdsQuery(List<BookId> BookIds) : IRequest<ErrorOr<List<Book>>>;
}

