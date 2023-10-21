using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface ITasksRepository
    {
        Task<TaskEntity> GetTask(string taskId);
        Task<bool> CreateTask(TaskEntity taskEntity);
        Task<bool> UpdateTask(TaskEntity taskEntity);
        Task<bool> DeleteTask(TaskEntity taskEntity);
        IQueryable<TaskEntity> GetAllTasks();
    }
}
