using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserManagementPortal.Modals;

namespace UserManagementPortal.Controllers
{
    [Authorize(Roles = UserRoles.Administrator)]
    [Route("[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private static readonly List<User> users = new List<User>()
        {
           new User{ Id = 1, Name = "User1"},
           new User{ Id = 2, Name = "User2"},
           new User{ Id = 3, Name = "User3"}
        };

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }
    }
}
