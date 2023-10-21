using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext _context;

        public TasksRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            SeedData();
        }

        public async Task<TaskEntity> GetTask(string taskId)
        {
            return await _context.Tasks.SingleOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<bool> CreateTask(TaskEntity taskEntity)
        {
            _context.Add(taskEntity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTask(TaskEntity taskEntity)
        {
            _context.Update(taskEntity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTask(TaskEntity taskEntity)
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private void SeedData()
        {
            //if the there are no users, seed the database with mock data
            if (_context.Tasks.Count() == 0)
            {
                _context.AddRange(MockData.GetMockTasks());
                _context.SaveChanges();
            }
        }
    }
}
