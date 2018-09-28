using System.Collections.Generic;
using tibs.stem.Auditing.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
