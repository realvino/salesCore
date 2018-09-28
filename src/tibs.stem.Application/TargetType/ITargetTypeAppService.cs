using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.TargetType.Dto;

namespace tibs.stem.TargetType
{
    public  interface ITargetTypeAppService : IApplicationService
    {
        ListResultDto<TargetTypeListDto> GetTargetType(GetTargetTypeInput input);
        Task<PagedResultDto<TargetTypeListDto>> GetTargetType1(GetTargetTypeInput1 input);
        Task<GetTargetType> GetTargetTypeForEdit(EntityDto input);
        Task CreateOrUpdateTargetType(CreateTargetTypeInput input);
        Task GetDeleteTargetType(EntityDto input);
    }
}
