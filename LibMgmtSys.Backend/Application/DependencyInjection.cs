using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand;
using MediatR;

namespace LibMgmtSys.Backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAuthorCommandHandler).Assembly));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}