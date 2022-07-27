using AutoMapper;
using Identity.Application;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;



//============Add services to the container=========================//
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration,environment.IsProduction());


//=========================AUTH and JWT=================================//
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


//=========================CORS=================================//
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

//====================Configure the HTTP request pipeline===================//

app.UseHttpsRedirection();

app.UseCors("corsapp");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<Identity.Infrastructure.SyncDataServices.Grpc.GrpcUserService>();
    endpoints.MapGrpcService<Identity.Infrastructure.SyncDataServices.Grpc.GrpcFriendService>();
});

Identity.Infrastructure.Data.PreDb.InitializeDB(app, environment.IsProduction());
app.Run();

