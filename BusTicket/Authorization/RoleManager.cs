using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ContactManager.Authorization
{

    public class RoleManager
    {
        public static readonly string ContactAdministratorsRole =
                                                              "ContactAdministrators";
        public static readonly string ContactManagersRole = "ContactManagers";
    }
}