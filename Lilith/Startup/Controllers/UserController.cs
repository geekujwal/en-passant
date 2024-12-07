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
    public async Task<bool> Ping()
    {
        await _userService.TestApiAsync(default);
        return true;
    }
}
