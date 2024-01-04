using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

builder.Configuration.AddEnvironmentVariables();

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
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new RsaSecurityKey(rsa),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

await app.UseOcelot();

app.Run();