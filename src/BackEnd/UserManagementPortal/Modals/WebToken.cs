using System;

namespace UserManagementPortal.Modals
{
    public class WebToken
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
