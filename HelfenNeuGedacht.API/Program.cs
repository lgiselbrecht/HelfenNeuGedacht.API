using HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDbContext<MySqlDbContext>(options =>
        options.UseMySQL(connectionString));
}
else
{
    throw new InvalidOperationException("Missing required database connection string: 'DefaultConnection'.");
}




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MySqlDbContext>();

    try
    {
        if (!db.Database.CanConnect())
        {
            throw new Exception("Unable to connect to the database.");
        }

        Console.WriteLine("Database connection successful.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database error during application startup:");
        Console.WriteLine(ex.Message);
        throw; // bricht den Startup komplett ab
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapScalarApiReference();

//app.UseAuthorization();

app.MapControllers();

app.Run();
