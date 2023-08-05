using System.Text;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Common.Interfaces.Services;
using LibMgmtSys.Backend.Infrastructure.Authentication;
using LibMgmtSys.Backend.Infrastructure.Persistence.Repositories;
using LibMgmtSys.Backend.Infrastructure.Persistence;
using LibMgmtSys.Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibMgmtSys.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistence();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        
        return services;
    }
    
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<LibMgmtSysDbContext>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        
        configuration.Bind(nameof(JwtSettings.SectionName), jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        
        return services;
    }
}