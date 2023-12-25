using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using R54_M11_Class_06_Work_01.HostedServices;
using R54_M11_Class_06_Work_01.Models;
using R54_M11_Class_06_Work_02.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDbContext>(op => op.UseSqlServer(
    builder.Configuration.GetConnectionString("db"), b=>b.MigrationsAssembly("R54_M11_Class_06_Work_01") ));
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(
    builder.Configuration.GetConnectionString("identity")));
builder.Services.AddScoped<IdentityDbInitializer>();
builder.Services.AddHostedService<IdentityInitializerHostedService>();
#region CORS Configuration
builder.Services.AddCors(options => {
    options.AddPolicy("EnableCORS",
        builder => {
            builder
                .WithOrigins("http://localhost:4200", "http://localhost:5454")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
        });
});
#endregion
#region Identity Configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>(op =>
{
    op.Password.RequiredLength = 6;
    op.Password.RequireDigit = false;
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireUppercase = false;
    op.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
#endregion
#region Authentication/JWT Configuration
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = builder. Configuration["Jwt:Site"],
            ValidIssuer = builder.Configuration["Jwt:Site"],
            IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"] ?? "IsDB-BISEW ESAD-54"))
        };
    });
#endregion
builder.Services.AddControllers();
var app = builder.Build();
app.UseStaticFiles();
app.UseCors("EnableCORS");
app.MapControllers();


app.Run();
