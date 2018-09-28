using Microsoft.Extensions.Configuration;

namespace tibs.stem.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
