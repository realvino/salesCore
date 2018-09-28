using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ActivityTypes.Dto;
using tibs.stem.Dto;

namespace tibs.stem.ActivityTypes
{
    public interface IActivityTypeAppService : IApplicationService
    {
        Task<FileDto> GetActivityTypeToExcel();
        ListResultDto<ActivityTypeListDto> GetActivityType(GetActivityTypeInput input);
        //ListResultDto<ActivityTypeListDto> GetActivityTypeList();  //(EntityDto input);
        Task<GetActivityType> GetActivityTypeForEdit(EntityDto input);
        Task CreateOrUpdateActivityType(CreateActivityTypeInput input);
        Task GetDeleteActivityType(EntityDto input);

    }
}
