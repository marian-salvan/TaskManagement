using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;

        public UsersController(ILogger<UsersController> logger, IUsersService usersService)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]string id)
        {
            return Ok(Task.FromResult(0));
        }
    }
}
