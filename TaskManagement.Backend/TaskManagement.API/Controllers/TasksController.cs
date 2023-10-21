using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITasksService _tasksService;

        public TasksController(ILogger<TasksController> logger, ITasksService tasksService)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tasksService = tasksService ?? throw new ArgumentNullException(nameof(tasksService));
        }

    }
}
