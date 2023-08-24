using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand;
using LibMgmtSys.Backend.Application.Common.Behaviors;
using LibMgmtSys.Backend.Application.Common.Interfaces.Services;
using MediatR;

namespace LibMgmtSys.Backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            //services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}