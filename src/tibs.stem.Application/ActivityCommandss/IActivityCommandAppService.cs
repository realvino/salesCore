using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ActivityCommandss.Dto;

namespace tibs.stem.ActivityCommandss
{
   public interface IActivityCommandAppService : IApplicationService
    {
        ListResultDto<ActivityCommandListDto> GetActivityCommand(GetActivityCommandInput input);
        Task CreateActivityCommand(CreateActivityCommandInput input);
        Task DeleteActivityCommand(EntityDto input);

    }
}
