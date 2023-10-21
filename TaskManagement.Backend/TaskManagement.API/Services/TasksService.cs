using AutoMapper;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.API.Services
{
    public class TasksService : ITasksService
    {      
        private readonly ITasksRepository _tasksRepository;
        private readonly ILogger<TasksService> _logger;    
        private readonly IMapper _mapper;

        public TasksService(ITasksRepository tasksRepository, ILogger<TasksService> logger, IMapper mapper)
        {
            _tasksRepository = tasksRepository ?? throw new ArgumentNullException(nameof(tasksRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
