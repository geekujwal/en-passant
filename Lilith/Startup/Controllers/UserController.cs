using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EnPassant.Lilith.Contracts;
using EnPassant.Lilith.Services;

namespace EnPassant.Lilith.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("RequestRegistrationToken")]
    public Task<StatusCodeResult> Ping()
    {
        _userService.TestApiAsync(default);
        return Task.FromResult<StatusCodeResult>(new NoContentResult());
    }
}
