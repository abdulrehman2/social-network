using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPrivacy>()
                .HasKey(o => new { o.UserId, o.PrivacyRuleId });
        }


        public DbSet<User> Users { get; set; }
        public DbSet<FriendShipRequest> FriendShipRequests { get; set; }
        public DbSet<AudienceType> Audiences { get; set; }
        public DbSet<PrivacyRule> PrivacyRules { get; set; }
        public DbSet<UserPrivacy> UserPrivacies { get; set; }


    }
}
