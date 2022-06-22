using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditosCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "CONGELADO", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
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

            

            return Summaries.Select(s => new WeatherForecast()
            {
                Date = DateTime.Now.AddDays(rng.Next(1,3)),
                TemperatureC = rng.Next(-10,35),
                Summary = s
            }).ToArray();

           
        }

        [HttpGet("test")]
        public IActionResult GetConnection()
        {
            try
            {
                return Ok("Con.." + Environment.GetEnvironmentVariable(Program.EntornoConexion)?.Substring(0, 80));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
