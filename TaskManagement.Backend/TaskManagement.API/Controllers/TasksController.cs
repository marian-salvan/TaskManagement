using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.API.Controllers
{
    //TODO: add security to the controller (authorize)
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ODataController
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITasksService _tasksService;

        public TasksController(ILogger<TasksController> logger, ITasksService tasksService)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tasksService = tasksService ?? throw new ArgumentNullException(nameof(tasksService));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]string id)
        {
            return Ok(await _tasksService.GetTask(id));
        }
    }
}
