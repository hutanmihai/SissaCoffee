using Microsoft.AspNetCore.Mvc;
using SissaCoffee.Helpers.Attributes;
using SissaCoffee.Models.DTOs.User;
using SissaCoffee.Services.UserService;

namespace SissaCoffee.Controllers;


[Route("api/users/")]
[ApiController]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("me")]
    [Authorization("Customer")]
    public async Task<ActionResult<UserDTO>> GetMe()
    {
        var userId = HttpContext.Items["UserId"].ToString();
        if (userId is null)
        {
            return NotFound();
        }
        var user = await _userService.GetUserDtoByIdAsync(new Guid(userId));
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}