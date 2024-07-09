using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repositories
{
    public class RepositoryContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User {get;set;}
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes {get;set;}

#pragma warning disable CS8618
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
        {

        }
#pragma warning restore CS8618
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}