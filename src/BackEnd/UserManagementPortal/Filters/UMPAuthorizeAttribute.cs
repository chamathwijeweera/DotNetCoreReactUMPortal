using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using UserManagementPortal.Modals;
using UserManagementPortal.Services;

namespace UserManagementPortal.Filters
{
    public class UMPAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public Modules Modules { get; set; }
        public Operations Operations { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var authService = context.HttpContext.RequestServices.GetService<IAuthService>();

                var isAthorized = authService.Authorize(context.HttpContext.User, Operations, Modules).Result;

                if (!isAthorized)
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                    return;
                }
            }
        }
    }
}
