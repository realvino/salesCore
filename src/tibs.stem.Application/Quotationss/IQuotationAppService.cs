using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Quotationss
{
    public interface IQuotationAppService : IApplicationService
    {
        Task<PagedResultDto<QuotationList>> GetQuotation(GetQuotationInput input);
        Task<GetQuotation> GetQuotationForEdit(NullableIdDto input);
        Task <int> CreateOrUpdateQuotation(CreateQuotationInput input);
        void UpdateQuotationTotal(UpdateQuotationTotal input);
        Task DeleteQuotation(EntityDto input);
        ListResultDto<QuotationProductList> GetQuotationProduct(GetQuotationsInput input);
        Task<GetQuotationProduct> GetQuotationProductForEdit(NullableIdDto input);
        Task CreateOrUpdateQuotationProduct(CreateQuotationProductInput input);
        Task DeleteQuotationProduct(EntityDto input);
        ListResultDto<QuotationServiceList> GetQuotationService(GetQuotationsInput input);
        Task<GetQuotationService> GetQuotationServiceForEdit(NullableIdDto input);
        Task CreateOrUpdateQuotationService(CreateQuotationServiceInput input);
        Task DeleteQuotationService(EntityDto input);
        Task<GetQuotationPreview> GetQuotationPreviewForEdit(NullableIdDto input);
        Task<PagedResultDto<QuotationList>> GetSalesOrder(GetQuotationInput input);

    }
}
