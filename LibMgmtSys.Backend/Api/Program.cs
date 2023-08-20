using LibMgmtSys.Backend.Api;
using LibMgmtSys.Backend.Infrastructure;
using LibMgmtSys.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using LibMgmtSys.Backend.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configuration.AddJsonFile("appsettings.Development.json");
    builder.Services
        //.AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddCors(options =>
            {
                options.AddPolicy(
                    "AllowSpecialAccess",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            }
        )
        //.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("AllowSpecialAccess");
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

