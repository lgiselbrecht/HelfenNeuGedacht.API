
using HelfenNeuGedacht.API.Application.Mapper;
using HelfenNeuGedacht.API.Application.Repositories;
using HelfenNeuGedacht.API.Application.Services;
using HelfenNeuGedacht.API.Application.Services.Auth.AuthService;
using HelfenNeuGedacht.API.Application.Services.AuthService;
using HelfenNeuGedacht.API.Application.Services.EventsService;
using HelfenNeuGedacht.API.Application.Services.OrganizationService;
using HelfenNeuGedacht.API.Application.Services.ShiftServices;
using HelfenNeuGedacht.API.Domain.Entities;
using HelfenNeuGedacht.API.Infrastructure.Repositories.MySqlRepository;
using HelfenNeuGedacht.API.Infrastructure.Seed;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//Für Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//geändert für httpsdocker
builder.Services.AddOpenApi();


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, MySqlEventsRepository>();

builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IShiftRepository, MysqlShiftRepository>();

builder.Services.AddScoped<IOrganizationRepository, MysqlOrganizationRepository>();

builder.Services.AddScoped<IOrganizationService, OrganizationService>();

builder.Services.AddTransient<DtoMapper>();




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

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<MySqlDbContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, JWTService>();

//// JWT Authentication Configuration
////TODO: checken obs das braucht
//var jwtSecret = builder.Configuration.GetSection("JwtSettings")["Secret"]
//    ?? throw new InvalidOperationException("JWT Secret not found in configuration");

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
//        ClockSkew = TimeSpan.Zero
//    };
//});

//HTTPS Support behind reverse proxy (e.g. nginx)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = scope.ServiceProvider.GetRequiredService<MySqlDbContext>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    try
    {
        if (!db.Database.CanConnect())
        {
            throw new Exception("Unable to connect to the database.");
        }

        Console.WriteLine("Database connection successful.");
      
        await db.Database.MigrateAsync();
        await IdentitySeeder.SeedRolesAsync(roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database error during application startup:");
        Console.WriteLine(ex.Message);
        throw; 
    }
}


app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "HelfenNeuGedacht API";

});

app.UseHttpsRedirection();


//TODO: besser einschränken 
app.UseCors("AllowAll");

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
