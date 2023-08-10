using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetAllBooksQuery
{
    public record GetAllBooksWithPaginationQuery(
        int PageNumber,
        int PageSize
        ) : IRequest<ErrorOr<List<Book>>>;
}

