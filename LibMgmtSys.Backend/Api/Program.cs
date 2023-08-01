using LibMgmtSys.Backend.Api;
using LibMgmtSys.Backend.Infrastructure;
using LibMgmtSys.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Configuration.AddJsonFile("appsettings.Development.json");
    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddPresentation()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

