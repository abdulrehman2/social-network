using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Post.Domain.Entities;

namespace Post.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<Post.Domain.Entities.Post> Posts { get; set; }  
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostReact> PostReactes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReactType> ReactTypes { get; set; }

    }
}
