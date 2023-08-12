using LibMgmtSys.Backend.Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace LibMgmtSys.Backend.Application.Common.Interfaces.Authorization
{
    public interface IJwtTokenDecoder
    {
        string? GetBearerTokenFromHeader(string authorizationHeader);
        DecodedJwtToken DecodeJwtToken(string token);
    }
}
