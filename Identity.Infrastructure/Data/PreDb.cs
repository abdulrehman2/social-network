using Identity.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Data
{
    public static class PreDb
    {

        public static void InitializeDB(IApplicationBuilder app, bool isProd)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetService<AppDbContext>(), isProd);

            }
        }
        private static bool SeedData(AppDbContext dbContext, bool isProd)
        {
            //if (isProd)
            //{
            try
            {
                Console.WriteLine("--> Attempting to perform migration");
                dbContext.Database.Migrate();

            }
            catch (Exception e)
            {
                Console.WriteLine("--> Unable to perform migration");
            }
            //}


            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(new User { FirstName = "Super", LastName = "Admin", CreatedDate = System.DateTime.Now, Email = "admin@gmail.com", Password = "password" });
                dbContext.SaveChanges();
                return true;
            }

            else
            {
                Console.WriteLine("Data populated already");
                return false;
            }

            //populate audiences
            if (!dbContext.Audiences.Any())
            {
                dbContext.Audiences.Add(new AudienceType { Id = 1, Description = "Only Me" });
                dbContext.Audiences.Add(new AudienceType { Id = 2, Description = "Friends" });
                dbContext.Audiences.Add(new AudienceType { Id = 3, Description = "Public" });
                dbContext.Audiences.Add(new AudienceType { Id = 4, Description = "Friend of Friends" });
                dbContext.SaveChanges();
            }

            if (!dbContext.PrivacyRules.Any())
            {
                dbContext.PrivacyRules.Add(new PrivacyRule { Id = 1, Code = "Introduction", Description = "Who can view your basic introduction" });
                dbContext.PrivacyRules.Add(new PrivacyRule { Id = 2, Code = "Posts", Description = "Who can view your Posts" });
                dbContext.PrivacyRules.Add(new PrivacyRule { Id = 3, Code = "Portfolio", Description = "Who can view your Portfolio" });
                dbContext.PrivacyRules.Add(new PrivacyRule { Id = 4, Code = "Friends", Description = "Who can view your Friends" });
                dbContext.SaveChanges();
            }

        }
    }
}
