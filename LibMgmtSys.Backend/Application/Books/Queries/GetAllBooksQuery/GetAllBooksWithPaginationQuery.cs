using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetAllBooksQuery
{
    public record GetAllBooksWithPaginationQuery() : IRequest<ErrorOr<List<Book>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    };
}

