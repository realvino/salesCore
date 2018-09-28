using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Warrantyss.Dto;

namespace tibs.stem.Warrantyss
{
    public interface IWarrantyAppService : IApplicationService
    {
        ListResultDto<WarrantyListDto> GetWarranty(GetWarrantyInput input);
        Task<GetWarranty> GetWarrantyForEdit(NullableIdDto input);
        Task CreateOrUpdateWarranty(CreateWarrantyInput input);
        Task GetDeleteWarranty(EntityDto input);

    }
}
