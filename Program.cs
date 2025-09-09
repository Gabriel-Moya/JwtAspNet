using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AuthService>();

var app = builder.Build();

app.MapGet("/", (AuthService service) => service.CreateToken());

app.Run();
