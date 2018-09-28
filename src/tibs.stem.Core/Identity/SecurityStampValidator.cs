using Abp.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using tibs.stem.Authorization.Roles;
using tibs.stem.Authorization.Users;
using tibs.stem.MultiTenancy;

namespace tibs.stem.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<IdentityOptions> options, 
            SignInManager signInManager) 
            : base(options, signInManager)
        {
        }
    }
}