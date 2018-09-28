using System.Threading.Tasks;
using Abp.Application.Services;
using tibs.stem.Configuration.Host.Dto;

namespace tibs.stem.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
