using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.Dto;
using tibs.stem.MileStoneStatuss.Dto;
using tibs.stem.MileStoneStatuss.Exporting;

namespace tibs.stem.MileStoneStatuss
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Master_MileStoneStatus)]
    public class MileStoneStatusAppService : stemAppServiceBase, IMileStoneStatusAppService
    {

        private readonly IRepository<MileStoneStatus> _mileStoneStatusRepository;
        private readonly IMileStoneStatusExcelExporter _mileStoneStatusExcelExporter;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public MileStoneStatusAppService(
            IRepository<MileStoneStatus> mileStoneStatusRepository,
            IMileStoneStatusExcelExporter mileStoneStatusExcelExporter,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _mileStoneStatusRepository = mileStoneStatusRepository;
            _mileStoneStatusExcelExporter = mileStoneStatusExcelExporter;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<FileDto> GetMileStoneStatusToExcel()
        {
            var currencyquery = from c in _mileStoneStatusRepository.GetAll()
                                select new MileStoneStatusListDto
                                {

                                    Id = c.Id,
                                    Code = c.Code,
                                    Name = c.Name
                                };


            var MileStoneStatusListDtos = currencyquery.MapTo<List<MileStoneStatusListDto>>();

            return _mileStoneStatusExcelExporter.ExportToFile(MileStoneStatusListDtos);
        }

        public ListResultDto<MileStoneStatusListDto> GetMileStoneStatus(GetMileStoneStatusInput input)
        {
            var Currency = _mileStoneStatusRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Name.Contains(input.Filter) ||
                         p.Code.Contains(input.Filter)
                )
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Code)
                .ToList();

            return new ListResultDto<MileStoneStatusListDto>(Currency.MapTo<List<MileStoneStatusListDto>>());
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_MileStoneStatus_Edit)]
        public async Task<GetMileStoneStatus> GetMileStoneStatusForEdit(EntityDto input)
        {
            var output = new GetMileStoneStatus
            {
            };

            var status = _mileStoneStatusRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Currency = status.MapTo<CreateMileStoneStatusInput>();

            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_MileStoneStatus_Create)]
        public async Task CreateOrUpdateMileStoneStatus(CreateMileStoneStatusInput input)
        {
            if (input.Id != 0)
            {
                await UpdateMileStoneStatus(input);
            }
            else
            {
                await CreateMileStoneStatus(input);
            }
        }

        public virtual async Task CreateMileStoneStatus(CreateMileStoneStatusInput input)
        {
            input.TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                DateTime myUtcDateTime = DateTime.Now;
                var status = input.MapTo<MileStoneStatus>();
                var val = _mileStoneStatusRepository
                  .GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();
                if (val == null)
                {
                    await _mileStoneStatusRepository.InsertAsync(status);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in MileStoneStatus Name '" + input.Name + "' or MileStoneStatus Code '" + input.Code + "'...");
                }
            }
        }

        public virtual async Task UpdateMileStoneStatus(CreateMileStoneStatusInput input)
        {
            input.TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                var status = input.MapTo<MileStoneStatus>();
                var val = _mileStoneStatusRepository
                  .GetAll().Where(p => (p.Code == input.Code || p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();
                if (val == null)
                {
                    await _mileStoneStatusRepository.UpdateAsync(status);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in MileStoneStatus Name '" + input.Name + "' or MileStoneStatus Code '" + input.Code + "'...");
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_MileStoneStatus_Delete)]
        public async Task DeleteMileStoneStatus(EntityDto input)
        {
            await _mileStoneStatusRepository.DeleteAsync(input.Id);
        }
    }
}
