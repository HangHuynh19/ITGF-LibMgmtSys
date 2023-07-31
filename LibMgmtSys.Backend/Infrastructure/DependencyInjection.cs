using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using LibMgmtSys.Backend.Infrastructure.Persistence;
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
        return services;
    }
