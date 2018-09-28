using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Productss.Dto;

namespace tibs.stem.Productss
{
   public interface IProductAppService : IApplicationService
    {
        Task<FileDto> GetProductToExcel();
        Task<PagedResultDto<ProductListDto>> GetProduct(GetProductInput input);
        Task<GetProduct> GetProductForEdit(EntityDto input);
        Task CreateOrUpdateProduct(CreateProductInput input);
        Task GetDeleteProduct(EntityDto input);
        ListResultDto<PriceLevelProductList> GetProductLevelPriceList(PriceLevelProductListInput input);
        Task<GetPriceLevelProduct> GetPriceLevelProductForEdit(NullableIdDto input);
        Task CreateOrUpdatePriceLevelProduct(CreatePriceLevelProductInput input);
        Task DeletePriceLevelProduct(EntityDto input);
    }
}
