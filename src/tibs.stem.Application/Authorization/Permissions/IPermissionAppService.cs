using Abp.Application.Services;
using Abp.Application.Services.Dto;
using tibs.stem.Authorization.Permissions.Dto;

namespace tibs.stem.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
