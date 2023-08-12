using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authentication;
using LibMgmtSys.Backend.Application.Common.Interfaces.Services;
using LibMgmtSys.Backend.Domain.UserAggregate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibMgmtSys.Backend.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _dateTimeProvider;
        
        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions, IDateTimeProvider dateTimeProvider)
        {
            _jwtSettings = jwtOptions.Value;
            _dateTimeProvider = dateTimeProvider;
        }

        public string GenerateToken(User user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name) 
            };
            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}

