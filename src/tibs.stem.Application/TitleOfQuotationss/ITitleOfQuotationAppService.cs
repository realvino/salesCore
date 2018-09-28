using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.TitleOfQuotationss.Dto;

namespace tibs.stem.TitleOfQuotationss
{
    public interface ITitleOfQuotationAppService : IApplicationService
    {
        ListResultDto<TitleOfQuotationList> GetTitleOfQuotation(GetTitleOfQuotationInput input);
        Task<GetTitleOfQuotation> GetTitleOfQuotationForEdit(NullableIdDto input);
        Task CreateOrUpdateTitleOfQuotation(TitleOfQuotationInput input);
        Task GetDeleteTitleOfQuotation(EntityDto input);
    }
}
