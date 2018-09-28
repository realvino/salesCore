using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using tibs.stem.Authorization.Users.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Authorization.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);
        Task<FileDto> GetUsersToExcel();
        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);
        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input);
        Task ResetUserSpecificPermissions(EntityDto<long> input);
        Task UpdateUserPermissions(UpdateUserPermissionsInput input);
        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);
        Task DeleteUser(EntityDto<long> input);
        Task UnlockUser(EntityDto<long> input);
        ListResultDto<TargetListDto> GetTarget(GetTargetInput input);
        Task<PagedResultDto<TargetListDto>> GetTarget1(GetTargetInput1 input);
        Task<GetTarget> GetTargetForEdit(GetTargetEditInput input);
        Task CreateOrUpdateTarget(CreateTargetInput input);
        Task GetDeleteTarget(EntityDto input);
    }
}