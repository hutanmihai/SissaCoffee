using Microsoft.AspNetCore.Mvc;
using SissaCoffee.Helpers.Attributes;

namespace SissaCoffee.Controllers;

[Route("api/test/")]
[ApiController]
public class TestController: ControllerBase
{
    [HttpGet("admin")]
    [Authorization("Admin")]
    public IActionResult Admin()
    {
        return Ok("Admin");
    }
        
    [HttpGet("customer")]
    [Authorization("Customer")]
    public IActionResult Customer()
    {
        return Ok("Customer");
    }
        
    [HttpGet("admin_and_customer")]
    [Authorization("Admin", "Customer")]
    public IActionResult AdminAndCustomer()
    {
        return Ok("Admin and Customer");
    }
}