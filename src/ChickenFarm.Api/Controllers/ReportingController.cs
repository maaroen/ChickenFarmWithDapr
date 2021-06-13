using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace ChickenFarm.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportingController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices]DaprClient daprClient)
        {
            try
            {
                var weatherData =
                    await daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "chickenfarm-reporting-api",
                        "WeatherForecast");
                return new OkObjectResult(weatherData);
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return new BadRequestObjectResult(exception.Message);
            }
        }
    }
}