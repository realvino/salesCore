using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Reasons.Dto;

namespace tibs.stem.Reasons
{
    public interface IReasonAppService
    {
        Task<PagedResultDto<ReasonListDto>> GetReason(GetReasonInput input);
        Task<GetReason> GetReasonForEdit(EntityDto input);
        Task CreateOrUpdateReason(CreateReasonInput input);
        Task GetDeleteReason(EntityDto input);
    }
}
