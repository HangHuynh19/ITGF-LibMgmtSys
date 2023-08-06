using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Books.Queries.GetBookByIdQuery
{
    public record GetBookByIdQuery() : IRequest<ErrorOr<Book>>
    {
        public BookId Id { get; set; }
    };
}

