using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Common.Interfaces.Services;
using LibMgmtSys.Backend.Infrastructure.Persistence.Repositories;
using LibMgmtSys.Backend.Infrastructure.Persistence;
using LibMgmtSys.Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace LibMgmtSys.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<LibMgmtSysDbContext>();
        /* (options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Meovacarot8.");
        }); */
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}