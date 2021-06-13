using System;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChickenFarm.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("Hoi!");
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(TaskDto task, [FromServices] DaprClient daprClient)
        {
            try
            {
                await daprClient.PublishEventAsync("pubsub", "tasks", task);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
                return new BadRequestObjectResult(exception.Message);
            }
        }
    }
}