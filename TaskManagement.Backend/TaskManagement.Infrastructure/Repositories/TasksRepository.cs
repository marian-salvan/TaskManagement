using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext _context;

        public TasksRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
            _context.Remove(taskEntity);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<TaskEntity> GetAllTasks()
        {
            return  _context.Tasks.AsQueryable();
        }
    }
}
