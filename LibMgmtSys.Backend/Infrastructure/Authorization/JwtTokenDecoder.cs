using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authorization;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace LibMgmtSys.Backend.Infrastructure.Authorization
{
    public class JwtTokenDecoder : IJwtTokenDecoder
    {
        public DecodedJwtToken DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = Guid.Parse(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);
            var firstName = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.GivenName).Value;
            var lastName = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.FamilyName).Value;
            var role = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
            
            return DecodedJwtToken.Create(userId, firstName, lastName, role);
        }
        
        public string? GetBearerTokenFromHeader(string authorizationHeader)
        {
            if (!authorizationHeader.StartsWith("Bearer "))
                return null;
            
            var bearerToken = authorizationHeader.Split(" ")[1];
            return bearerToken;
        }
    }
}