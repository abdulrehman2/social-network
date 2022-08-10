using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kafka.Connector;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Post.Application.Contracts.Repositories;
using Post.Application.Contracts.Services;
using Post.Application.Contracts.SyncDataServices.Grpc;
using Post.Infrastructure.Data;
using Post.Infrastructure.Repositories;
using Post.Infrastructure.Services;
using Post.Infrastructure.SyncDataServices.Grpc;

namespace Post.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, bool isProduction)
        {

            var connectionString = configuration.GetConnectionString("UserDBConnection");
            if (isProduction)
            {
                Console.WriteLine("--> Using MySQL Container");
                services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                         mySqlOptions =>
                         {

                             mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                         });
                });
            }
            else
            {
                Console.WriteLine("--> Using MySQL Local Database");
                services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                         mySqlOptions =>
                         {

                             mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                         });
                });
            }



            //================REPOSITORIES==================//
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostCommentRepository, PostCommentRepository>();
            services.AddTransient<IPostReactRepository, PostReactRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();


            //================SERVICES==================//
            services.AddTransient<IFileStorage, FileStorage>();


            //================GRPC==================//
            services.AddScoped<IUserDataClient, UserDataClient>();
            services.AddScoped<IFriendDataClient, FriendDataClient>();


            //==============MESSAGE BUS=============//
            services.AddKafkaServices(configuration, isProduction);


            //==============EVENT PROCESSOR===============//
            services.AddScoped<Application.Contracts.EventProcessing.IEventProcessor,EventProcessing.EventProcessor>();


            //==============ASYNC DATA SERVICES =============//
             services.AddHostedService<BackgroundServices.KafkaEventListener>();

            return services;
        }
    }
}
