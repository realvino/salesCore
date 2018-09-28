using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace tibs.stem.Authorization.Roles
{
    public static class AppRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //Static host roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host)
                );

            //Static tenant roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Admin,
                    MultiTenancySides.Tenant)
                );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.User,
                    MultiTenancySides.Tenant)
                );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.GeMan,
                    MultiTenancySides.Tenant)
                );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.AssMan,
                    MultiTenancySides.Tenant)
                );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.SaMan,
                    MultiTenancySides.Tenant)
                );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Exec,
                    MultiTenancySides.Tenant)
                );
        }
    }
}
