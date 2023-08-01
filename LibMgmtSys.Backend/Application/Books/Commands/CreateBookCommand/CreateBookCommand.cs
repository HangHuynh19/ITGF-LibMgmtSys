using ErrorOr;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookReviewAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.ValueObjects;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;
using MediatR;

namespace LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand
{
  public record CreateBookCommand(
    string Title,
    string Isbn,
    string Publisher,
    List<AuthorId> AuthorIds,
    int Year,
    //List<GenreId> GenreIds,
    string Description,
    Uri Image,
    int BorrowingPeriod,
    int Quantity
    //List<BookReviewId> BookReviewIds
  ) : IRequest<ErrorOr<Book>>;
}