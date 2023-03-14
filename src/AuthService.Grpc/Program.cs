using AuthService.BusinessLogic.Models.AppSettings;
using AuthService.DataAccess.Extensions;
using AuthService.Grpc.Infrastructure.Configurations;
using AuthService.Grpc.Interceptors;
using AuthService.Grpc.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args)
    .AddAppSettings()
    .AddSerilogLogger();

var configuration = builder.Configuration;

builder.Services.Configure<ServiceEndpointsSettings>(
    builder.Configuration.GetSection("ServiceEndpointsSettings"));

builder.Services.Configure<JwtConfig>(
    builder.Configuration.GetSection("JwtConfig"));

builder.Services
    .AddProviders()
    .AddRepositories()
    .AddPostgresContext(options =>
    {
        var connectionString = configuration.GetConnectionString("AuthDb");
        options.UseNpgsql(connectionString, options => options.EnableRetryOnFailure(3));
    })
    .AddInfrastructureServices()
    .AddGrpcClients()
    .AddBusinessLogicServices()
    .AddFluentValidation()
    .AddJwtServices(configuration)
    .AddGrpc(options =>
    {
        options.Interceptors.Add<ErrorHandlingInterceptor>();
        options.Interceptors.Add<ValidationInterceptor>();
    });

var app = builder.Build();

app.MapGrpcService<AuthService.Grpc.Services.AuthService>();

app.Run();

namespace AuthService.Grpc
{
    public partial class Program
    { }
}
