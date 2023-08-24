using ErrorOr;
using MediatR;

using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand
{
  public record CreateAuthorCommand(
    string Name,
    string Biography
  ) : IRequest<ErrorOr<Author>>;
}