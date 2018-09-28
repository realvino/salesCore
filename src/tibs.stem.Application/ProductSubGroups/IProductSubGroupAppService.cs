using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductSubGroups.Dto;

namespace tibs.stem.ProductSubGroups
{
    public interface IProductSubGroupAppService : IApplicationService
    {
        Task<FileDto> GetProductSubGroupToExcel();
        Task<PagedResultDto<ProductSubGpListDto>> GetProductSubGroup(GetProductSubGpInput input);
        Task<GetProductSubGp> GetProductSubGroupForEdit(EntityDto input);
        Task CreateOrUpdateProductSubGroup(CreateProductSubGpInput input);
        Task GetDeleteProductSubGroup(EntityDto input);
    }
}
