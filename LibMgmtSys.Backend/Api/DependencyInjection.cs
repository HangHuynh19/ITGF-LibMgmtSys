using LibMgmtSys.Backend.Api.Common.Mapping;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authorization;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Infrastructure.Authorization;
using LibMgmtSys.Backend.Infrastructure.Persistence;
using LibMgmtSys.Backend.Infrastructure.Persistence.Repositories;

namespace LibMgmtSys.Backend.Api
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
      services.AddSingleton<IJwtTokenDecoder, JwtTokenDecoder>();
      services.AddControllers();
      services.AddMapping();
      return services;
    }
  }
}