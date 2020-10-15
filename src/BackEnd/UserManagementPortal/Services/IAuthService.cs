using System.Security.Claims;
using System.Threading.Tasks;
using UserManagementPortal.Modals;

namespace UserManagementPortal.Services
{
    public interface IAuthService
    {
        Task<bool> Authorize(ClaimsPrincipal claimsPrincipal, Operations operation, Modules modules);
    }
}