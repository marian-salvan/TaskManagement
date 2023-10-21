using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Extensions
{
    public static class TaskEntityModelBuilder
    {
        public static void ConfigTaskEntityColumns(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>().HasKey(c => c.Id);

            modelBuilder.Entity<TaskEntity>()
                .Property(b => b.Name)
                .IsRequired();

            modelBuilder.Entity<TaskEntity>()
                .Property(b => b.Status)
                .IsRequired();

            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskEntity>()
                .ToTable("Tasks");
        }
    }
}
