using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Authentication.Common;

namespace LibMgmtSys.Backend.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
        ) : IRequest<ErrorOr<AuthenticationResult>>;
}

