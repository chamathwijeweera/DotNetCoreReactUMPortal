using System;

namespace UserManagementPortal.Modals
{
    [Flags]
    public enum Operations : int
    {
        Create = 1,
        Read = 2,
        Update = 3,
        Delete = 4,
        Execute = 5
    }

    [Flags]
    public enum Modules : int
    {
        Administrator = 11,
        Manager = 12,
        Developer = 13,
        Customer = 14,
    }
}
