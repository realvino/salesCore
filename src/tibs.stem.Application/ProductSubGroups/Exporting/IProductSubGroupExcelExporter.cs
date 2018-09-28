using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.ProductSubGroups.Dto;

namespace tibs.stem.ProductSubGroups.Exporting
{
    public interface IProductSubGroupExcelExporter
    {
        FileDto ExportToFile(List<ProductSubGpListDto> ProductSubGpListDtos);
    }
}
