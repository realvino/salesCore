using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tibs.stem.Tenants.Dashboard.Dto;

namespace tibs.stem.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();

        GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input);

        GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input);

        GetWorldMapOutput GetWorldMap(GetWorldMapInput input);

        GetServerStatsOutput GetServerStats(GetServerStatsInput input);

        GetGeneralStatsOutput GetGeneralStats(GetGeneralStatsInput input);
        ListResultDto<TenantTargetListDto> GetTenantTarget(GetTenantTargetInput input);
        Task<PagedResultDto<TenantTargetListDto>> GetTenantTargetPagedResult(GetTenantTargetInput1 input);
        Task<GetTenantTarget> GetTenantTargetForEdit(EntityDto input);
        Task CreateOrUpdateTenantTarget(CreateTenantTargetInput input);
        Task GetDeleteTarget(EntityDto input);
        Task<Array> GetActivepotential();  // Task<PagedResultDto<SpResultActivepotential>> GetActivepotential();
        Task<Array> GetUserTarget(EntityDto input);       // Task<PagedResultDto<SpResultUserTarget>> GetUserTarget();
        Task<List<SliderDataList>> GetSalesExecutive();
        Task CreateOrUpdateTenantVatAmount(SettingVatAmountInput input);
        Task<SettingVatAmountInput> GetTenantVatAmountSettingForEdit(NullableIdDto input);
    }
}
