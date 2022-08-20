using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var DatosAmbiente = new StringBuilder();
                var data = string.Empty;

                data = $" EntornoConexion {Environment.GetEnvironmentVariable(Program.EntornoConexion)} ";
                DatosAmbiente.Append(data);

                data = $" EntornoConexion Process {Environment.GetEnvironmentVariable(Program.EntornoConexion, EnvironmentVariableTarget.Process)} ";
                DatosAmbiente.Append(data);

                data = $" EntornoConexion Machine {Environment.GetEnvironmentVariable(Program.EntornoConexion, EnvironmentVariableTarget.Process)} ";
                DatosAmbiente.Append(data);

                data = $" EntornoConexion User {Environment.GetEnvironmentVariable(Program.EntornoConexion, EnvironmentVariableTarget.Process)} ";
                DatosAmbiente.Append(data);

                data = $" entorno establecido {Database.SqlDataContext.fileNameDatabase}";
                DatosAmbiente.Append(data);

                Program._logger.Info("Consultando datos de entorno");
                return Ok(DatosAmbiente.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
