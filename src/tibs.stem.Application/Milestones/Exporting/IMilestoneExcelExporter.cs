using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.MileStones.Dto;

namespace tibs.stem.Milestones.Exporting
{
    public interface IMilestoneExcelExporter
    {
        FileDto ExportToFile(List<MileStoneListDto> MileStoneListDtos);
    }
}
