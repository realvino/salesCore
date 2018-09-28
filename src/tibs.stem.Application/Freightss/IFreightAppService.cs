using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Freightss.Dto;

namespace tibs.stem.Freightss
{
    public interface IFreightAppService : IApplicationService
    {
        ListResultDto<FreightListDto> GetFreight(GetFreightInput input);
        Task<GetFreight> GetFreightForEdit(NullableIdDto input);
        Task CreateOrUpdateFreight(CreateFreightInput input);
        Task GetDeleteFreight(EntityDto input);
    }
}
