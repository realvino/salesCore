using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using tibs.stem.Configuration;

namespace tibs.stem.Web.Configuration
{
    public class AppConfigurationAccessor: IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public AppConfigurationAccessor(IHostingEnvironment env)
        {
            Configuration = env.GetAppConfiguration();
        }
    }
}
