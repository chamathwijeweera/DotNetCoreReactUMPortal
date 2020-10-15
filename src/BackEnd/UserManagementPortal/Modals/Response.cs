using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace UserManagementPortal.Modals
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<IdentityError> Errors { get; set; }
    }
}