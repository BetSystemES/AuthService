using AuthService.BusinessLogic.Models.AppSettings;
using AuthService.DataAccess.Extensions;
using AuthService.Grpc.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args)
    .AddAppSettings()
    .AddSerilogLogger();

var configuration = builder.Configuration;

builder.Services.Configure<JwtConfig>(
    builder.Configuration.GetSection("JwtConfig"));

builder.Services
    .AddProviders()
    .AddRepositories()
    .AddPostgresContext(options =>
    {
        var connectionString = configuration.GetConnectionString("AuthDb");
        options.UseNpgsql(connectionString);
    })
    .AddInfrastructureServices()
    .AddBusinessLogicServices()
    .AddJwtServices(configuration)
    .AddGrpc();

var app = builder.Build();

app.MapGrpcService<AuthService.Grpc.Services.AuthService>();

app.Run();

namespace AuthService.Grpc
{
    public partial class Program { }
}
