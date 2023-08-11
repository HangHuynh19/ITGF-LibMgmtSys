using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand
{
    public record UpdateUserCommand(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<ErrorOr<User>>;
}