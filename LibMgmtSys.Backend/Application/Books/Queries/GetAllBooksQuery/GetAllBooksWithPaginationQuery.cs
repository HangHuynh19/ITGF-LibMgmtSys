using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetAllBooksQuery
{
    public record GetAllBooksWithPaginationQuery(
        int PageNumber,
        int PageSize,
        string SortOrder,
        string SearchTerm
        ) : IRequest<ErrorOr<List<Book>>>;
}

