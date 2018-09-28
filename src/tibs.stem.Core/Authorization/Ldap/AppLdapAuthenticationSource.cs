#if FEATURE_LDAP
using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using tibs.stem.Authorization.Users;
using tibs.stem.MultiTenancy;

namespace tibs.stem.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
#endif