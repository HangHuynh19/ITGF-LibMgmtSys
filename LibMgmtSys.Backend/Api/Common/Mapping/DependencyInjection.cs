using System.Reflection;
using Mapster;
using MapsterMapper;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
      var config = new TypeAdapterConfig();
      config.Scan(Assembly.GetExecutingAssembly());
      config.Apply(new AuthorMappingConfig());
      services.AddSingleton(config);
      services.AddScoped<IMapper, ServiceMapper>();
      return services;
    }
  }
}