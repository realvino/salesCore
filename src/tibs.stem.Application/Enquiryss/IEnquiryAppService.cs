using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Enquiryss.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Enquiryss
{
    public interface IEnquiryAppService : IApplicationService
    {
        Task<Array> GetEnquiryQuotationKanbanOld(EnquiryKanbanInput input);
        Task<FileDto> GetEnquiryToExcel();
        Task<PagedResultDto<EnquiryList>> GetEnquiryGrid(EnquiryListInput input);
        ListResultDto<EnquiryList> GetNotificationEnquiry();
        Task<Array> GetEnquiryKanban(EnquiryKanbanInput input);
        Task CreateOrUpdateEnquiry(EnquiryInput input);
        Task<GetEnquiry> GetEnquiryForEdit(NullableIdDto input);
        Task EnquiryKanbanUpdateAsync(EnquiryKanbanUpdateInput input);
        Task EnquiryQuotationKanbanUpdateAsync(EnquiryQuotationKanbanUpdateInput input);
        Task<PagedResultDto<QuotationList>> GetEnquiryQuotation(GetEnquiryQuotationInput input);
        Task<Array> GetEnquiryQuotationsKanban(EnquiryKanbanInput input);
        Task GetDeleteEnquiry(EntityDto input);
        Task<Array> GetInquiryKanban(EnquiryKanbanInput input);
        Task<PagedResultDto<EnquiryQuotationKanbanList>> GetGlobalReport(EnquiryListInput input);
        Task<FileDto> GetGlobalReportToExcel();
    }
}
