using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Packingss.Dto;

namespace tibs.stem.Packingss
{
    public interface IPackingAppService : IApplicationService
    {
        ListResultDto<PackingListDto> GetPacking(GetPackingInput input);
        Task<GetPacking> GetPackingForEdit(NullableIdDto input);
        Task CreateOrUpdatePacking(CreatePackingInput input);
        Task GetDeletePacking(EntityDto input);
    }
}
