using System.Threading.Tasks;
using Abp.Application.Services;
using tibs.stem.Configuration.Tenants.Dto;

namespace tibs.stem.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
