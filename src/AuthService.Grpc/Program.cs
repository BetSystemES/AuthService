using AuthService.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddGrpc();

builder.Services.Configure<JWTConfig>(
    builder.Configuration.GetSection("JWTConfig"));

var app = builder.Build();

app.Run();
