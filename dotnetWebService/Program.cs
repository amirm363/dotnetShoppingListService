using System;
using System.IO;
using dotnetWebService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySqlConnector;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Register DbContext
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 35))));

builder.Services.AddHealthChecks().AddDbContextCheck<MyDbContext>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use CORS with the "AllowAll" policy
app.UseCors("AllowAll");

// Get logger from DI
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Use logger to test database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
    try
    {
        // Perform a database operation to ensure the connection works
        dbContext.Database.OpenConnection();
        dbContext.Database.CloseConnection();
        logger.LogInformation("Successfully connected to the database.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Could not connect to the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


