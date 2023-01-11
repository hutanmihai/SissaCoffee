using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SissaCoffee.Models;

namespace SissaCoffee.Helpers.Attributes
{
    public class AuthorizationAttribute:Attribute, IAuthorizationFilter
    {
        private readonly ICollection<ApplicationRole> _roles;

        public AuthorizationAttribute(params ApplicationRole[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedStatusObject = new JsonResult(new { Message = "Unauthorized" })
            { StatusCode = StatusCodes.Status401Unauthorized };

            if(_roles == null)
            {
                context.Result = unauthorizedStatusObject;
            }
            
            ApplicationUser? user = context.HttpContext.Items["ApplicationUser"] as ApplicationUser;

            if (user == null || !user.Roles.Any(role => _roles.Contains(role)))
            {
                context.Result = unauthorizedStatusObject;
            }
        }
    }
}
