using TaskManagement.Core.Requests;
using TaskManagement.Core.Responses;

namespace TaskManagement.Core.Interfaces
{
    public interface ITasksService
    {
        Task<GetTaskResponse> GetTask(string taskId);
        Task<GetTaskResponse> CreateTask(CreateUpdateTaskRequest taskRequest);
        Task<bool> UpdateTask(string taskId, CreateUpdateTaskRequest taskRequest);
        Task<bool> DeleteTask(string taskId);
        Task<string> GenerateTaskSummary(string taskId);
    }
}
