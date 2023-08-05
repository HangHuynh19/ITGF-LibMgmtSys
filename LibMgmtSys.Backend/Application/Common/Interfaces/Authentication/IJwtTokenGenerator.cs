using LibMgmtSys.Backend.Domain.UserAggregate;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}

