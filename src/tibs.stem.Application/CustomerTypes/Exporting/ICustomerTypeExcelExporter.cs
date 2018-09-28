using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Dto;
using tibs.stem.NewCustomerTypes.Dto;

namespace tibs.stem.CustomerTypes.Exporting
{
    public interface ICustomerTypeExcelExporter
    {
        FileDto ExportToFile(List<NewCustomerTypeListDto> CustomerTypeListDtos);
    }
}
