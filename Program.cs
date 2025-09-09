using JwtAspNet;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AuthService>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (AuthService service) => {
    var user = new User(
        1,
        "Gabriel Moya",
        "example@email.com",
        "https://exampleimage.com/image.jpg",
        "supersecretPassword",
        ["student", "premium"]);

    return service.CreateToken(user);
});

app.MapGet("/restrito", () => "Access Granted").RequireAuthorization();

app.Run();
