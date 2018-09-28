using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Milestones.Dto;
using tibs.stem.MileStones.Dto;

namespace tibs.stem.MileStones
{
    public interface IMilestoneAppService : IApplicationService
    {
        Task<FileDto> GetMilestoneToExcel();
        ListResultDto<MileStoneListDto> GetMilestones(GetMileStoneInput input);
        Task<GetMileStone> GetMilestoneForEdit(EntityDto input);
        Task CreateOrUpdateMilestone(CreateMileInputDto input);
        Task GetDeleteMilestone(EntityDto input);



        //ListResultDto<MileStoneDetailListDto> GetMileStoneDetailList(EntityDto input);
        Task<GetMilestoneDetail> GetMileStoneDetailForEdit(EntityDto input);        
        Task CreateOrUpdateMileStoneDetail(CreateMileStoneDetailInput input);
        Task GetDeleteMileStoneDetail(EntityDto input);
       

    } 
}
