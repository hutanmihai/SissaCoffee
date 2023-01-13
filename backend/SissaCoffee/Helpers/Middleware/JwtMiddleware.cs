using Microsoft.AspNetCore.Identity;
using SissaCoffee.Helpers.JwtUtils;
using SissaCoffee.Models;
using SissaCoffee.Services.UserService;

namespace SissaCoffee.Helpers.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtMiddleware(RequestDelegate next, UserManager<ApplicationUser> userManager)
        {
            _next = next;
            _userManager = userManager;
        }

        public async Task Invoke(HttpContext httpContext, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId = jwtUtils.ValidateJwtToken(token);

            if(userId != Guid.Empty)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    httpContext.Items["Roles"] = roles;
                }
            }

            await _next(httpContext);
        }
    }
}
