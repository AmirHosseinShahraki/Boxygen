using Authentication.API.Utilities;
using Authentication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();