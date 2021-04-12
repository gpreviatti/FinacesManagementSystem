using Data.Mapping;
using Data.Seeds;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add your entities below
            modelBuilder.Entity<User>(new UserMap().Configure);

            // Add your seeders below
            UserSeeder.Users(modelBuilder);
        }
    }
}
