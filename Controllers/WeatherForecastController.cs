using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a_aspnetcore_rest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// https://www.strathweb.com/2015/01/asp-net-mvc-6-attribute-routing-controller-action-tokens/
// https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-3.1

namespace a_aspnetcore_rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // [controller] = Controller name
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("[action]/{name}")] // [action] = function name
        public string Hello(string name)
        {
            return "hello " + name;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<string> HelloAsync() // HelloAsync ==> [action] = hello, otherwise full name
        {
            return await Task.Run(() => "hi Async");
        }
    }
}
