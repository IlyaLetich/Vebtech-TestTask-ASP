using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using TestTask.Data.Entitys;
using TestTask.Data.Enums;

namespace TestTask.Data.DataContexts
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roles = new List<Role>
            {
                new Role(UserRole.User,"User"),
                new Role(UserRole.Admin,"Admin"),
                new Role(UserRole.Support, "Support"),
                new Role(UserRole.SuperAdmin, "SuperAdmin")
            };

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .UsingEntity("UsersToRolesJoinTable");

            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}

