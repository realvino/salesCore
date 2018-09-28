using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Activitys.Dto;

using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Uow;
using tibs.stem;
using tibs.stem.Activitys;

namespace tibs.sc.Activitys
{
    public class ActivityAppService : stemAppServiceBase, IActivityAppService
    {

        public readonly IRepository<Activity> _ActivityRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ActivityAppService(IRepository<Activity> ActivityRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _ActivityRepository = ActivityRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public ListResultDto<ActivityListDto> GetActivity(GetActivityInput input)
        {
            int TenantId =  (int)(AbpSession.TenantId);
            using (_unitOfWorkManager.Current.SetTenantId(TenantId))
            {
                var Activity = _ActivityRepository.GetAll()
                 .WhereIf(
                 !input.Filter.IsNullOrEmpty(),
                 p => p.ActivityTypes.Name.Contains(input.Filter) ||
                     p.Contacts.Name.Contains(input.Filter) ||
                     p.Enquirys.EnquiryNo.Contains(input.Filter) ||
                     p.Title.Contains(input.Filter)
                 );


                var query = from s in Activity
                            select new ActivityListDto
                            {
                                ActTypeName = s.ActivityTypes.Name != null ? s.ActivityTypes.Name : "",
                                Title = s.Title != null ? s.Title : "",
                                Description = s.Description != null ? s.Description : "",
                                EnquiryNo = s.Enquirys.EnquiryNo != null ? s.Enquirys.EnquiryNo : "",
                                ScheduleTime = s.ScheduleTime != null ? s.ScheduleTime : null,
                                Status = s.Status,
                                ContactName = s.Contacts.Name ?? "",
                                Id = s.Id,
                                ActivityTypeId = s.ActivityTypeId,
                                EnquiryId = s.EnquiryId != null ? s.EnquiryId : null,
                                ContactId = s.ContactId != null ? s.ContactId : null
                            };

                var list = query.ToList();
                return new ListResultDto<ActivityListDto>(list.MapTo<List<ActivityListDto>>());
            }
            
        }
        public ListResultDto<ActivityListDto> GetEnquiryWiseActivity(EntityDto input)
        {
                int TenantId = (int)(AbpSession.TenantId);
                using (_unitOfWorkManager.Current.SetTenantId(TenantId))
                {
                    var Activity = _ActivityRepository
                 .GetAll().Where(p => p.EnquiryId == input.Id);

                    var query = from s in Activity
                                select new ActivityListDto
                                {
                                    ActTypeName = s.ActivityTypes.Name != null ? s.ActivityTypes.Name : "",
                                    Title = s.Title != null ? s.Title : "",
                                    Description = s.Description != null ? s.Description : "",
                                    EnquiryNo = s.Enquirys.EnquiryNo != null ? s.Enquirys.EnquiryNo : "",
                                    ScheduleTime = s.ScheduleTime != null ? s.ScheduleTime : null,
                                    Status = s.Status,
                                    ContactName = s.Contacts.Name ?? "",
                                    Id = s.Id,
                                    ActivityTypeId = s.ActivityTypeId,
                                    EnquiryId = s.EnquiryId != null ? s.EnquiryId : null,
                                    ContactId = s.ContactId != null ? s.ContactId : null
                                };
                    var list = query.ToList();
                    return new ListResultDto<ActivityListDto>(list.MapTo<List<ActivityListDto>>());
                }
            
        }

        public async Task<GetActivity> GetActivityForEdit(EntityDto input)
        {
            var output = new GetActivity();
                if (input.Id > 0)
                {
                    var query = _ActivityRepository
                        .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

                    output.Activitys = query.MapTo<CreateActivityInput>();

                }
            return output;
        }

        public async Task CreateOrUpdateActivity(CreateActivityInput input)
        {

                if (input.Id == 0)
                {
                    await CreateActivity(input);
                }
                else
                {
                    await UpdateActivity(input);
                }
        }

        public virtual async Task CreateActivity(CreateActivityInput input)
        {
            input.TenantId = (int)(AbpSession.TenantId);
            var Activity = input.MapTo<Activity>();   

                var query = _ActivityRepository.GetAll().Where(p => p.Title == input.Title).FirstOrDefault();
                if (query == null)
                {
                    await _ActivityRepository.InsertAsync(Activity);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Activity...");

                }
            

        }

        public virtual async Task UpdateActivity(CreateActivityInput input)
        {

                var Activity = input.MapTo<Activity>();

                var query = _ActivityRepository.GetAll()
                    .Where(p => p.Title == input.Title && p.Id != input.Id).FirstOrDefault();
                if (query == null)
                {
                    await _ActivityRepository.UpdateAsync(Activity);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Activity ...");

                }
        }

        public async Task GetDeleteActivity(EntityDto input)
        {
            await _ActivityRepository.DeleteAsync(input.Id);
        }

    }
}
