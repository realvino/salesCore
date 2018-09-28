using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using tibs.stem.Caching.Dto;

namespace tibs.stem.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultDto<CacheDto> GetAllCaches();

        Task ClearCache(EntityDto<string> input);

        Task ClearAllCaches();
    }
}
