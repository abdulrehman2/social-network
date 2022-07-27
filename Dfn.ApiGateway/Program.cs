using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;



//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
//app.Run();

namespace Dfn.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {

            new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddJsonFile("ocelot.json")
                    .AddEnvironmentVariables();
            })
            .ConfigureServices(s =>
            {


                var authenticationProviderKey = "IdentityApiKey";

                //var key = Convert.FromBase64String("placeholder-key-that-is-long-enough-for-sha256");

                s.AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(authenticationProviderKey, x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
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


                s.AddCors(p => p.AddPolicy("corsapp", builder =>
                {
                    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                }));


                s.AddOcelot();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //add your logging
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseCors("corsapp");
                app.UseStaticFiles();
                app.UseOcelot().Wait();
            })
            .Build()
            .Run();
        }
    }
}
