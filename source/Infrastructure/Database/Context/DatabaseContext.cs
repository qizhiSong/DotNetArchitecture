using Microsoft.EntityFrameworkCore;
using Solution.CrossCutting.Security;
using Solution.Model.Entities;
using Solution.Model.Enums;

namespace Solution.Infrastructure.Database
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Configure(modelBuilder);
            Seed(modelBuilder);
        }

        private static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserLogEntityTypeConfiguration());
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            var hash = new Hash();

            modelBuilder.Entity<UserEntity>().HasData(new UserEntity
            {
                UserId = 1,
                Name = "Administrator",
                Surname = "Administrator",
                Email = "administrator@administrator.com",
                Login = hash.Create("admin"),
                Password = hash.Create("admin"),
                Roles = Roles.User | Roles.Admin,
                Status = Status.Active
            });
        }
    }
}
