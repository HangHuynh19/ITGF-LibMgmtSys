using LibMgmtSys.Backend.Api.Common.Mapping;

namespace LibMgmtSys.Backend.Api
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
      services.AddControllers();
      services.AddMapping();

      return services;
    }
  }
}