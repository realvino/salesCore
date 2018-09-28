using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using tibs.stem.Authorization;

namespace tibs.stem
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(stemCoreModule)
        )]
    public class stemApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(stemApplicationModule).GetAssembly());
        }
    }
}