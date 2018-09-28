using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.ActivityTypes.Dto;
using tibs.stem.Dto;

namespace tibs.stem.ActivityTypes.Exporting
{
    public interface IActivityTypeExcelExporter
    {
        FileDto ExportToFile(List<ActivityTypeListDto> ActivityTypeListDtos);
    }
}
