using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Post.Application.Contracts.Repositories;
using Post.Application.Contracts.SyncDataServices.Grpc;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Data
{
    public static class PreDb
    {

        public static void InitializeDB(this IServiceProvider app, bool isProd)
        {
            using (var scope = app.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var grpcUserService = scope.ServiceProvider.GetService<IUserDataClient>();
                var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
                SeedData(dbContext, grpcUserService, userRepository, isProd);
            }
        }
        private static bool SeedData(AppDbContext dbContext,
            IUserDataClient userDataClient,
            IUserRepository userRepository,
            bool isProd)
        {

            #region Perofrm Migrations
            //if (isProd)
            //{
            try
            {
                Console.WriteLine("--> Attempting to perform migration");
                dbContext.Database.Migrate();


                if (!dbContext.ReactTypes.Any())
                {
                    dbContext.ReactTypes.Add(new ReactType { Id = 1, Description = "Like" });
                    dbContext.ReactTypes.Add(new ReactType { Id = 2, Description = "Loved" });
                    dbContext.ReactTypes.Add(new ReactType { Id = 3, Description = "Laughed" });
                    dbContext.ReactTypes.Add(new ReactType { Id = 4, Description = "Hated" });
                    dbContext.SaveChanges();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("--> Unable to perform migration");
            }
            //}

            #endregion

            //#region Add Admin User (Default)

            //try
            //{

            //    if (!dbContext.Users.Any())
            //    {
            //        dbContext.Users.Add(new User { Id = 1, ExternalId = 1, Name = "Admin", CreatedDate = DateTime.Now });
            //        dbContext.SaveChanges();  
            //    }

            //    else
            //    {
            //        Console.WriteLine("Data populated already");
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}

            //#endregion

            #region Get All Users (Grpc)
            Console.WriteLine("--> Seeding missing users");
            var users = userDataClient.ReturnAllUsers();
            bool isSynced = userRepository.SyncMissingUsers(users);
            #endregion

            return true;

        }
    }
}
