using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Application.Contracts.Repositories;
using Identity.Application.Contracts.Services;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Identity.Infrastructure.SyncDataServices.Grpc;

namespace Identity.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
        {


            //================================Database=================================//
            var connectionString = configuration.GetConnectionString("UserDBConnection");
            if (isProduction)
            {
                Console.WriteLine("--> Using MySQL Container");
                services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            }
            else
            {
                Console.WriteLine("--> Using MySQL Local Database");
                //builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
                services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            }


            services.AddGrpc();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFriendshipRepository, FriendshipRepository>();

            //services.AddTransient<IFileStorage, FileStorage>();


            return services;
        }
    }
}
