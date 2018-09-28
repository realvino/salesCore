using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductGroups.Dto;

namespace tibs.stem.ProductGroups
{
     public interface IProductGroupAppService : IApplicationService
    {
        Task<FileDto> GetProductGroupToExcel();
        ListResultDto<ProductGroupListDto> GetProductGroup(GetProductGroupInput input);
        Task<GetProductGroup> GetProductGroupForEdit(NullableIdDto input);
        Task CreateOrUpdateProductGroup(CreateProductGroupInput input);
        Task GetDeleteProductGroup(EntityDto input); 
    
    }
}
