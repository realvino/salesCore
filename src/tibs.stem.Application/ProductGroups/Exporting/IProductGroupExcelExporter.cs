using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.ProductGroups.Dto;

namespace tibs.stem.ProductGroups.Exporting
{
   public interface IProductGroupExcelExporter
    {
        FileDto ExportToFile(List<ProductGroupListDto> ProductGroupListDtos);

    }
}
