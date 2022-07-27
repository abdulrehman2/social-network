using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Post.Api.Helpers;
using Post.Application;
using Post.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;



// Add services to the container.
builder.Services.AddControllers()
//    (options =>
//{
//    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
//})
.AddNewtonsoftJson();
    

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration, environment.IsProduction());

var authenticationProviderKey = "IdentityApiKey";
builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(authenticationProviderKey, cfg =>
             {
                 cfg.RequireHttpsMetadata = true;
                 cfg.SaveToken = true;
                 cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                 {
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256")),
                     ValidateAudience = false,
                     ValidateIssuer = false,
                     ValidateLifetime = false,
                     RequireExpirationTime = false,
                     ClockSkew = TimeSpan.Zero,
                     ValidateIssuerSigningKey = true
                 };
             });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

Post.Infrastructure.Data.PreDb.InitializeDB(app.Services, environment.IsProduction());
app.Run();






