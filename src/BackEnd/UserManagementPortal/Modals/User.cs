using System.Collections.Generic;

namespace UserManagementPortal.Modals
{
    public class User : BaseModal
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public List<UserPermission> Permissions { get; set; }
    }
}
