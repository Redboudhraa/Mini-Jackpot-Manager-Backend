using Microsoft.EntityFrameworkCore;
using MiniJackpotManager.Data;
using MiniJackpotManager.Data.Repositories;
using MiniJackpotManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
var reactAppUrl = builder.Configuration["CorsOrigins:ReactApp"] ?? "http://localhost:3000";


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(reactAppUrl) // React default port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure DbContext
builder.Services.AddDbContext<JackpotDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Register repositories and services
builder.Services.AddScoped<IJackpotRepository, JackpotRepository>();
builder.Services.AddScoped<IJackpotService, JackpotService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Apply migrations in development
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<JackpotDbContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
