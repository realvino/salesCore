using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Activitys.Dto;

namespace tibs.sc.Activitys
{
    public interface IActivityAppService : IApplicationService
    {
        ListResultDto<ActivityListDto> GetActivity(GetActivityInput input);
        ListResultDto<ActivityListDto> GetEnquiryWiseActivity(EntityDto input);
        Task<GetActivity> GetActivityForEdit(EntityDto input);
        Task CreateOrUpdateActivity(CreateActivityInput input);
        Task GetDeleteActivity(EntityDto input);

    }
}
