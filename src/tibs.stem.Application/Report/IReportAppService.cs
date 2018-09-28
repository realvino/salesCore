using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Report.Dto;

namespace tibs.stem.Report
{
    public interface IReportAppService : IApplicationService
    {
        Task<PagedResultDto<ReportList>> GetQuotationSubmittedReport(GetReportInput input);
        Task<PagedResultDto<ReportList>> GetQuotationWonReport(GetReportInput input);
        Task<PagedResultDto<ReportList>> GetQuotationLostReport(GetReportInput input);
        Task<PagedResultDto<QuotationReportDtoList>> GetQuotationReport(GetReportInput input);
        Task<PagedResultDto<QuotationReportDtoList>> GetInquiryReport(GetReportInput input);
        Task<PagedResultDto<CompanyReportList>> GetCompanyReport(GetReportInput input);
        Task<PagedResultDto<CompanyReportList>> GetContactReport(GetReportInput input);
        Task<FileDto> GetSubmittedReportToExcel();
        Task<FileDto> GetWonReportToExcel();
        Task<FileDto> GetLostReportToExcel();
        Task CreateReport(ReportGeneratorInput input);

    }
}
