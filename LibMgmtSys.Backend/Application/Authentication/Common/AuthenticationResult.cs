using LibMgmtSys.Backend.Domain.UserAggregate;

namespace LibMgmtSys.Backend.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}

