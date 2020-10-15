using System.Collections.Generic;

namespace UserManagementPortal.Modals
{
    public class User : BaseModal
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> UserRoles { get; set; }
        public List<string> UserPermission { get; set; }
    }
}
