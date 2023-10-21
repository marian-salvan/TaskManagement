using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class UserEntityModelBuilder
    {
        public static void ConfigUserEntityColumns(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(c => c.Id);

            modelBuilder.Entity<UserEntity>()
                .Property(b => b.Name)
                .IsRequired();

            modelBuilder.Entity<UserEntity>()
                .Property(b => b.Email)
                .IsRequired();

            modelBuilder.Entity<UserEntity>()
                .ToTable("Users");
        }
    }
}
