using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using tibs.stem.Configuration;
using tibs.stem.MultiTenancy;

namespace tibs.stem.Web.Startup
{
    [DependsOn(
        typeof(stemWebCoreModule))]
    public class stemWebHostModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public stemWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = _appConfiguration["App:ServerRootAddress"] ?? "http://localhost:22742/";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(stemWebHostModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (IocManager.Resolve<IMultiTenancyConfig>().IsEnabled)
            {
                var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
                workManager.Add(IocManager.Resolve<SubscriptionExpirationCheckWorker>());
                workManager.Add(IocManager.Resolve<SubscriptionExpireEmailNotifierWorker>());
            }
        }
    }
}
