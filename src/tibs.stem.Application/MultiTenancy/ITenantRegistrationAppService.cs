using System.Threading.Tasks;
using Abp.Application.Services;
using tibs.stem.Editions.Dto;
using tibs.stem.MultiTenancy.Dto;
using tibs.stem.Select2;
using tibs.stem.MileStones.Dto;

namespace tibs.stem.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
        Task<Select2Result> GetTenantType();
        Task CreateMilestone(CreateMileInputDto input);
        Task<CheckEmailValid> EmailValidation(EmailCheck input);
    }
}