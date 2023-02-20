using AuthService.BusinessLogic;
using AuthService.Grpc.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args)
    .AddAppSettings();

var configuration = builder.Configuration;

builder.Services
    .AddInfrastructureServices()
    .AddBusinessLogicServices()
    .AddJwtServices(configuration)
    .AddGrpc();

builder.Services.Configure<JWTConfig>(
    builder.Configuration.GetSection("JWTConfig"));

var app = builder.Build();

app.Run();
