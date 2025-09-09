using JwtAspNet.Models;
using JwtAspNet.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AuthService>();

var app = builder.Build();

app.MapGet("/", (AuthService service) => {
    var user = new User(
        1,
        "Gabriel Moya",
        "example@email.com",
        "https://exampleimage.com/image.jpg",
        "supersecretPassword",
        new[] { "student", "premium" });

    return service.CreateToken(user);
});

app.Run();
