using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Authentication.Common;

namespace LibMgmtSys.Backend.Application.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}

