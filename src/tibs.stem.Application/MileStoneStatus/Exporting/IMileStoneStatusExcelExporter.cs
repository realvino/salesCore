using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.MileStoneStatuss.Dto;

namespace tibs.stem.MileStoneStatuss.Exporting
{
    public interface IMileStoneStatusExcelExporter
    {
        FileDto ExportToFile(List<MileStoneStatusListDto> MileStoneStatusListDtos);
    }
}
