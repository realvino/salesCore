using System.Threading.Tasks;
using Abp.Configuration;

namespace tibs.stem.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
