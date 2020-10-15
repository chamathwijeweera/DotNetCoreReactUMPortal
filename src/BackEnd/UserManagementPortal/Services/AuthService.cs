using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagementPortal.Data;
using UserManagementPortal.Modals;

namespace UserManagementPortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> Authorize(ClaimsPrincipal claimsPrincipal, Operations operation, Modules modules)
        {
            var user = await _userManager.FindByNameAsync(claimsPrincipal.Identity.Name);

            var userPermission = _context.UserModulePermissions.Where(e => e.UserId == user.Id).ToList();

            var entitled = userPermission.Where(e => e.OperationId == ((int)operation).ToString() && e.ModuleId == ((int)modules).ToString()).Any();

            return entitled;
        }
    }
}
