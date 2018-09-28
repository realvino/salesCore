using Abp.Authorization;
using tibs.stem.Authorization.Roles;
using tibs.stem.Authorization.Users;

namespace tibs.stem.Authorization
{
    /// <summary>
    /// Implements <see cref="PermissionChecker"/>.
    /// </summary>
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
