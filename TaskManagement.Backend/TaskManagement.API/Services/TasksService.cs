using AutoMapper;
using TaskManagement.Core.Constants;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Requests;
using TaskManagement.Core.Responses;

namespace TaskManagement.API.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ILogger<TasksService> _logger;
        private readonly IMapper _mapper;
        private readonly ITaskSummaryGenerator _summaryGenerator;

        public TasksService(ITasksRepository tasksRepository, ILogger<TasksService> logger, IMapper mapper, ITaskSummaryGenerator summaryGenerator)
        {
            _tasksRepository = tasksRepository ?? throw new ArgumentNullException(nameof(tasksRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _summaryGenerator = summaryGenerator ?? throw new ArgumentNullException(nameof(summaryGenerator));
        }

        public async Task<GetTaskResponse> GetTask(string taskId)
        {
            var taskEntity = await _tasksRepository.GetTask(taskId);

            return taskEntity is null ? 
                throw new Exception($"Could not find task for id {taskId}") : 
                _mapper.Map<GetTaskResponse>(taskEntity);
        }

        public async Task<GetTaskResponse> CreateTask(CreateUpdateTaskRequest taskRequest) 
        {
            var taskEntity = _mapper.Map<TaskEntity>(taskRequest);
            var saved = await _tasksRepository.CreateTask(taskEntity);

            //we can have specific exeption that can be handled in exeption middleware
            return !saved ?
                 throw new Exception("Could not save task") :
                _mapper.Map<GetTaskResponse>(taskEntity);
        }

        public async Task<bool> DeleteTask(string taskId)
        {
            var taskEntity = await _tasksRepository.GetTask(taskId);

            return taskEntity is null ?
                throw new Exception($"Could not find task for id {taskId}") :
                await _tasksRepository.DeleteTask(taskEntity);
        }

        public async Task<bool> UpdateTask(string taskId, CreateUpdateTaskRequest taskRequest)
        {
            var taskEntity = await _tasksRepository.GetTask(taskId);

            if (taskEntity is not null)
            {
                taskEntity.Name = taskRequest.Name;
                taskEntity.Description = taskRequest.Description;
                taskEntity.Status = taskRequest.Status;

                return await _tasksRepository.UpdateTask(taskEntity);
            }

            throw new Exception($"Could not find task for id {taskId}");
        }

        public async Task<string> GenerateTaskSummary(string taskId)
        {
            var taskEntity = await _tasksRepository.GetTask(taskId);

            if (taskEntity is null)
            {
                return MessageConstants.TaskSummaryNotFound;
            }

            var summary = await _summaryGenerator.GetTaskDescription();
            return $"Cat fact for {taskEntity.Name}: {summary}";
        }
    }
}
