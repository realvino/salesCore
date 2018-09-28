using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Currencyy.Dto;
using tibs.stem.Dto;
using tibs.stem.MileStoneStatuss.Dto;

namespace tibs.stem.MileStoneStatuss
{
    public interface IMileStoneStatusAppService : IApplicationService
    {
        Task<FileDto> GetMileStoneStatusToExcel();
        ListResultDto<MileStoneStatusListDto> GetMileStoneStatus(GetMileStoneStatusInput input);

        Task CreateOrUpdateMileStoneStatus(CreateMileStoneStatusInput input);

        Task<GetMileStoneStatus> GetMileStoneStatusForEdit(EntityDto input);

        Task DeleteMileStoneStatus(EntityDto input);
    }
}
