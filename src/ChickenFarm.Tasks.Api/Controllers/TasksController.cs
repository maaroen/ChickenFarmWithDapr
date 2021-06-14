using System.Threading.Tasks;
using ChickenFarm.Api;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChickenFarm.Tasks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [Topic("pubsub", "tasks")]
        [HttpPost("NewTaskPublished")]
        public async Task<IActionResult> Add(TaskDto task, [FromServices] DaprClient daprClient)
        {
            _logger.LogInformation($"TASKAPI: NEW TASK!: {task.Id}"); 
            return new OkObjectResult(task.Temperature);
        }
    }
}