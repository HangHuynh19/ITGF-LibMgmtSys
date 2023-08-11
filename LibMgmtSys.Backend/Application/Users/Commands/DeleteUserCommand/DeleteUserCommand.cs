using LibMgmtSys.Backend.Domain.UserAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace LibMgmtSys.Backend.Application.Users.Commands.DeleteUserCommand
{
    public record DeleteUserCommand(UserId UserId) : IRequest<ErrorOr<User>>;
}

