using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.Productss.Dto;

namespace tibs.stem.Productss.Exporting
{
    public interface IProductListExcelExporter
   {
        FileDto ExportToFile(List<ProductListDto> ProductListDtos);
    }
}
