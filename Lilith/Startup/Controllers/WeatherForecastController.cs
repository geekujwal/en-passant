using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Stella.Core.ErrorHandling;
using EnPassant.Lilith.Contracts;

namespace EnPassant.Lilith.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController()
    {
    }

    [HttpPost("RequestRegistrationToken")]
    public IEnumerable<WeatherForecast> Get()
    {
        throw new NotImplemented();
    }
}
