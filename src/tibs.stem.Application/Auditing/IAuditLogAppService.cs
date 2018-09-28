using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using tibs.stem.Auditing.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}