using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Security;
using Microsoft.EntityFrameworkCore;
using tibs.stem.Authorization;
using tibs.stem.Editions.Dto;
using tibs.stem.MultiTenancy.Dto;
using tibs.stem.Url;
using System.Data.SqlClient;
using System.Data;
using System;

namespace tibs.stem.MultiTenancy
{
   // [AbpAuthorize(AppPermissions.Pages_Tenants)]
    public class TenantAppService : stemAppServiceBase, ITenantAppService
    {
        public IAppUrlService AppUrlService { get; set; }
       
        public TenantAppService()
        {
            AppUrlService = NullAppUrlService.Instance;
        }

        public async Task<PagedResultDto<TenantListDto>> GetTenants(GetTenantsInput input)
        {

            var query = TenantManager.Tenants

                     .Include(t => t.Edition)

                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), t => t.Name.Contains(input.Filter) || t.TenancyName.Contains(input.Filter))

                .WhereIf(input.CreationDateStart.HasValue, t => t.CreationTime >= input.CreationDateStart.Value)

                .WhereIf(input.CreationDateEnd.HasValue, t => t.CreationTime <= input.CreationDateEnd.Value)

                .WhereIf(input.SubscriptionEndDateStart.HasValue, t => t.SubscriptionEndDateUtc >= input.SubscriptionEndDateStart.Value.ToUniversalTime())
                        .WhereIf(input.SubscriptionEndDateEnd.HasValue, t => t.SubscriptionEndDateUtc <= input.SubscriptionEndDateEnd.Value.ToUniversalTime())
                        .WhereIf(input.EditionIdSpecified, t => t.EditionId == input.EditionId);

            var querylst = from s in query
                           select new TenantListDto
                           {
                               TenancyName = s.TenancyName,
                               Name = s.Name,
                               EditionDisplayName = s.Edition.DisplayName,
                               ConnectionString = s.ConnectionString,
                               IsActive = s.IsActive,
                               CreationTime = s.CreationTime,
                               SubscriptionEndDateUtc = s.SubscriptionEndDateUtc,
                               TenantTypeName = s.TenantTypes.Name,
                               Id = s.Id
                           };
            var tenantCount = await querylst.CountAsync();

            var tenants = await querylst.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<TenantListDto>(tenantCount, ObjectMapper.Map<List<TenantListDto>>(tenants));

        }


        [AbpAuthorize(AppPermissions.Pages_Tenants_Create)]
        [UnitOfWork(IsDisabled = true)]
        public async Task CreateTenant(CreateTenantInput input)
        {
            await TenantManager.CreateWithAdminUserAsync(input.TenancyName,
                input.Name,
                input.AdminPassword,
                input.AdminEmailAddress,
                input.ConnectionString,
                input.IsActive,
                input.EditionId,
                input.ShouldChangePasswordOnNextLogin,
                input.SendActivationEmail,
                input.SubscriptionEndDateUtc?.ToUniversalTime(),
                input.IsInTrialPeriod,
                AppUrlService.CreateEmailActivationUrlFormat(input.TenancyName),
                input.TenantTypeId
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task<TenantEditDto> GetTenantForEdit(EntityDto input)
        {
            var tenantEditDto = ObjectMapper.Map<TenantEditDto>(await TenantManager.GetByIdAsync(input.Id));
            tenantEditDto.ConnectionString = SimpleStringCipher.Instance.Decrypt(tenantEditDto.ConnectionString);
            return tenantEditDto;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task UpdateTenant(TenantEditDto input)
        {
            await TenantManager.CheckEditionAsync(input.EditionId, input.IsInTrialPeriod);

            input.ConnectionString = SimpleStringCipher.Instance.Encrypt(input.ConnectionString);
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            ObjectMapper.Map(input, tenant);
            tenant.SubscriptionEndDateUtc = tenant.SubscriptionEndDateUtc?.ToUniversalTime();

            await TenantManager.UpdateAsync(tenant);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Delete)]
        public async Task DeleteTenant(EntityDto input)
        {
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            await TenantManager.DeleteAsync(tenant);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task<GetTenantFeaturesEditOutput> GetTenantFeaturesForEdit(EntityDto input)
        {
            var features = FeatureManager.GetAll();
            var featureValues = await TenantManager.GetFeatureValuesAsync(input.Id);

            return new GetTenantFeaturesEditOutput
            {
                Features = ObjectMapper.Map<List<FlatFeatureDto>>(features).OrderBy(f => f.DisplayName).ToList(),
                FeatureValues = featureValues.Select(fv => new NameValueDto(fv)).ToList()
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task UpdateTenantFeatures(UpdateTenantFeaturesInput input)
        {
            await TenantManager.SetFeatureValuesAsync(input.Id, input.FeatureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task ResetTenantSpecificFeatures(EntityDto input)
        {
            await TenantManager.ResetAllFeaturesAsync(input.Id);
        }

        public async Task UnlockTenantAdmin(EntityDto input)
        {
            using (CurrentUnitOfWork.SetTenantId(input.Id))
            {
                var tenantAdmin = await UserManager.FindByNameAsync(AbpUserBase.AdminUserName);
                if (tenantAdmin != null)
                {
                    tenantAdmin.Unlock();
                }
            }
        }
        public async Task<PagedResultDto<SpResultActivepotential>> GetActivepotential()
        {
            int TenantId =  (int)(AbpSession.TenantId);            
            long userid = (int)AbpSession.UserId;

            List<SpResultActivepotential> Activepotentiallist = new List<SpResultActivepotential>();
           
            ConnectionAppService db = new ConnectionAppService();   //var db = _scDbContext.Database.GetDbConnection().ConnectionString;
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("sp_activepotential", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@TenantId", TenantId);
                sqlComm.Parameters.AddWithValue("@UserId", userid);
                conn.Open();
                using (var reader = sqlComm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SpResultActivepotential actpotential = new SpResultActivepotential();
                        actpotential.createmonth = Convert.ToString(reader["create_month"]);
                        actpotential.totalcreatedvalue = Convert.ToDecimal(reader["total_created_value"]);
                        actpotential.totalwonvalue = Convert.ToDecimal(reader["total_won_value"]);
                        actpotential.optimalactive = Convert.ToDecimal(reader["optimal_active"]);
                        Activepotentiallist.Add(actpotential);
                    }
                }
                conn.Close();
            }

            var ActivepotentialCount = Activepotentiallist.Count;
           return new PagedResultDto<SpResultActivepotential>(ActivepotentialCount, Activepotentiallist);
            //return new PagedResultDto<SpResultActivepotential>(ActivepotentialCount, ObjectMapper.Map<List<SpResultActivepotential>>(Activepotentiallist));
        }
    }
    public class SpResultActivepotential
    {
        public string createmonth { get; set; }
        public decimal totalcreatedvalue { get; set; }
        public decimal totalwonvalue { get; set; }
        public decimal optimalactive { get; set; }


    }
}