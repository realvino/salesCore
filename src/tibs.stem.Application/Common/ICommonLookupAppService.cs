using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using tibs.stem.Common.Dto;
using tibs.stem.Editions.Dto;

namespace tibs.stem.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}