using LibMgmtSys.Backend.Api;
using LibMgmtSys.Backend.Infrastructure;
using LibMgmtSys.Backend.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configuration.AddJsonFile("appsettings.Development.json");
    builder.Services
        .AddSwaggerGen()
        .AddCors(options =>
            {
                options.AddPolicy(
                    "AllowSpecialAccess",
                    builder =>
                    {
                        builder
                            .WithOrigins(Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            }
        )
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

    app.Map("/api/v1/message", app =>
    {
        app.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"message\": \"Welcome to Story Vault Library Management System!\"}");
        });
    });
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

public partial class Program { }