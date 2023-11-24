using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile(
    $"ocelot.{environmentName}.json",
    optional: false,
    reloadOnChange: true
);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        var publicKey = builder.Configuration.GetValue<string>("JwtConfiguration:PublicKey");
        var rsa = RSA.Create();
        rsa.ImportFromPem(publicKey.ToCharArray());
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            RequireExpirationTime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new RsaSecurityKey(rsa),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseOcelot();

app.Run();