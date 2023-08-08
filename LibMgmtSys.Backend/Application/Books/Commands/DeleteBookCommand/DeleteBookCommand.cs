using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Books.Commands.DeleteBookCommand
{
    public record DeleteBookCommand(BookId BookId) : IRequest<ErrorOr<Book>>;
}
