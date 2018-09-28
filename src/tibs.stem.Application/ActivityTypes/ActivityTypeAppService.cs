using Abp.Application.Services.Dto;
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
using tibs.stem.ActivityTypes.Dto;
using tibs.stem;
using tibs.stem.ActivityTypes;
using tibs.stem.ActivityTypess;
using tibs.stem.ActivityTypes.Exporting;
using tibs.stem.Dto;
using Abp.Authorization;
using tibs.stem.Authorization;

namespace tibs.sc.ActivityTypes
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Master_ActivityType)]
    public class ActivityTypeAppService : stemAppServiceBase, IActivityTypeAppService
    {

        public readonly IRepository<ActivityType> _ActivityTypeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IActivityTypeExcelExporter _activityTypeExcelExporter;
        public ActivityTypeAppService(IActivityTypeExcelExporter activityTypeExcelExporter, IRepository<ActivityType> MilestoneStatusListRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _ActivityTypeRepository = MilestoneStatusListRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _activityTypeExcelExporter = activityTypeExcelExporter;
        }
        public async Task<FileDto> GetActivityTypeToExcel()
        {

            var query = _ActivityTypeRepository.GetAll();
            var data = (from s in query
                        select new ActivityTypeListDto
                        {
                            Code = s.Code,
                            Name = s.Name,
                            Id = s.Id
                        }).ToList();


            var ActivityTypeListDtos = data.MapTo<List<ActivityTypeListDto>>();

            return _activityTypeExcelExporter.ExportToFile(ActivityTypeListDtos);
        }
        public ListResultDto<ActivityTypeListDto> GetActivityType(GetActivityTypeInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var ActivityType = _ActivityTypeRepository.GetAll()
                 .WhereIf(
                 !input.Filter.IsNullOrEmpty(),
                 p => p.Name.Contains(input.Filter) ||
                     p.Code.Contains(input.Filter)
                 );


                var query = from s in ActivityType
                            select new ActivityTypeListDto
                            {
                                Code = s.Code,
                                Name = s.Name,
                                Id = s.Id
                            };
                var list = query.ToList();
                return new ListResultDto<ActivityTypeListDto>(list.MapTo<List<ActivityTypeListDto>>());
            }
        }
        //public ListResultDto<ActivityTypeListDto> GetActivityTypeList()  //(EntityDto input)
        //{
        //    try
        //    {
        //        var ActivityType = _ActivityTypeRepository.GetAll();
        //        //.GetAll().Where(p => p.Id == input.Id);

        //        var query = from s in ActivityType
        //                    select new ActivityTypeListDto
        //                    {
        //                        Code = s.Code,
        //                        Name = s.Name,
        //                        Id = s.Id
        //                    };
        //        var list = query.ToList();
        //        return new ListResultDto<ActivityTypeListDto>(list.MapTo<List<ActivityTypeListDto>>());
        //    }
        //    catch (Exception obj)
        //    {
        //        string ff = obj.Message.ToString();
        //        return null;
        //    }
        //}

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_ActivityType_Edit)]
        public async Task<GetActivityType> GetActivityTypeForEdit(EntityDto input)
        {
            var output = new GetActivityType();
            try
            {
                if (input.Id > 0)
                {
                    var query = _ActivityTypeRepository
                                   .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

                    output.ActivityTypes = query.MapTo<CreateActivityTypeInput>();

                }
            }
            catch (Exception obj)
            {
                string ff = obj.Message.ToString();
            }
            return output;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_ActivityType_Create)]
        public async Task CreateOrUpdateActivityType(CreateActivityTypeInput input)
        {
            int TenantId = (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                if (input.Id == 0)
                {
                    await CreateActivityType(input);
                }
                else
                {
                    await UpdateActivityType(input);
                }
            }
        }

        public virtual async Task CreateActivityType(CreateActivityTypeInput input)
        {

            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                input.TenantId = (int)(AbpSession.TenantId);
                var ActivityType = input.MapTo<ActivityType>();

                var query = _ActivityTypeRepository.GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();
                if (query == null)
                {
                    await _ActivityTypeRepository.InsertAsync(ActivityType);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Activity Type ...");
                }
            }

        }

        public virtual async Task UpdateActivityType(CreateActivityTypeInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                input.TenantId = (int)(AbpSession.TenantId);

                var ActivityType = input.MapTo<ActivityType>();

                var query = _ActivityTypeRepository.GetAll()
                    .Where(p => (p.Code == input.Code && p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();
                if (query == null)
                {
                    await _ActivityTypeRepository.UpdateAsync(ActivityType);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Activity Type ...");

                }
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Master_ActivityType_Delete)]
        public async Task GetDeleteActivityType(EntityDto input)
        {
            await _ActivityTypeRepository.DeleteAsync(input.Id);
        }

    }
}
