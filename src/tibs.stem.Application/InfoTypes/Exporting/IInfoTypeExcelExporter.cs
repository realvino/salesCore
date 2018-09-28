using System;
using System.Collections.Generic;
using System.Text;
using tibs.NewInfoTypes.Dto;
using tibs.stem.Dto;

namespace tibs.stem.InfoTypes.Exporting
{
    public interface IInfoTypeExcelExporter
    {
        FileDto ExportToFile(List<NewInfoTypeListDto> NewInfoTypeListDtos);
    }
}
