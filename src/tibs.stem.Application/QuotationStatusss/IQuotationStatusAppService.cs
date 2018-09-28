using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.QuotationStatusss.Dto;

namespace tibs.stem.QuotationStatusss
{
    public interface IQuotationStatusAppService : IApplicationService
    {
        ListResultDto<QuotationStatusList> GetQuotationStatus(GetQuotationStatusInput input);
        Task<GetQuotationStatus> GetQuotationStatusForEdit(NullableIdDto input);
        Task CreateOrUpdateQuotationStatus(CreateQuotationStatusInput input);
        Task GetDeleteQuotationStatus(EntityDto input);

    }
}
