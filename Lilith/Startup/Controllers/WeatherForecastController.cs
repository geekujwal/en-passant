using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Stella.Core.ErrorHandling;
using EnPassant.Lilith.Contracts;

namespace EnPassant.Lilith.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastController()
    {
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        throw new NotFoundException("Not found");
    }
}
