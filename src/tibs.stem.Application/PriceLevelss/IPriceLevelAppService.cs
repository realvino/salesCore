using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.PriceLevelss.Dto;

namespace tibs.stem.PriceLevelss
{
    public interface IPriceLevelAppService : IApplicationService
    {
        ListResultDto<PriceLevelList> GetPriceLevel(GetPriceLevelListInput input);
        Task<GetPriceLevel> GetPriceLevelForEdit(NullableIdDto input);
        Task CreateOrUpdatePriceLevel(PriceLevelCreate input);
        Task DeletePriceLevel(EntityDto input);

    }
}
