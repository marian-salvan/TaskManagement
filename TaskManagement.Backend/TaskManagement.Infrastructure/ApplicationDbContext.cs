using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Infrastructure.Extensions;

namespace TaskManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigTaskEntityColumns();
            modelBuilder.ConfigUserEntityColumns();


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
