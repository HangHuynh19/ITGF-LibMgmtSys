using LibMgmtSys.Backend.Domain.BookAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetAllBooksQuery
{
    public record GetAllBooksQuery() : IRequest<ErrorOr<List<Book>>>;
}

