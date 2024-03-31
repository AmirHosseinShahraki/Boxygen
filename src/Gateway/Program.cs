using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

builder.Configuration.AddEnvironmentVariables();

string? environmentName = builder.Environment.EnvironmentName;
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
        string? publicKey = builder.Configuration.GetValue<string>("JwtConfiguration:PublicKey");
        RSA rsa = RSA.Create();
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

WebApplication app = builder.Build();

await app.UseOcelot();

app.Run();