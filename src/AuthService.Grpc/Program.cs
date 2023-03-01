using AuthService.BusinessLogic;
using AuthService.DataAccess;
using AuthService.Grpc.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args)
    .AddAppSettings()
    .AddSerilogLogger();

var configuration = builder.Configuration;

// TODO: typo in JWTConfig
builder.Services.Configure<JWTConfig>(
    builder.Configuration.GetSection("JWTConfig"));

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
