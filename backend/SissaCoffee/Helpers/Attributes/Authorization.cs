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
            
            var actualRoles = context.HttpContext.Items["Roles"] as IList<String>;

            foreach (var role in _roles)
            {
                if (!actualRoles.Contains(role.ToString()))
                {
                    context.Result = unauthorizedStatusObject;
                }
            }
        }
    }
}
