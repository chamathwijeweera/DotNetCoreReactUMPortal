using System.Collections.Generic;
using System.Security.Claims;
using UserManagementPortal.Modals;

namespace UserManagementPortal.Infastructure
{
    public interface IJWTAuthManager
    {
        WebToken GenerateTokens(List<Claim> authClaims);
    }
}